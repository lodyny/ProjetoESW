﻿using AdotAqui.Data;
using AdotAqui.Models.Entities;
using AdotAqui.Models.ViewModels;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Application Controllers
/// </summary>
namespace AdotAqui.Controllers
{
    /// <summary>
    /// Controller used to manage all the animals
    /// </summary>
    public class AnimalsController : Controller
    {
        private readonly AdotAquiDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Animals Constructor
        /// </summary>
        /// <param name="context">AdotAquiDbContext</param>
        public AnimalsController(AdotAquiDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Used to obtain the form to create a new animal
        /// </summary>
        /// <returns>Create View</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var speciesSet = _context.AnimalSpecies;
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var viewModel = new AnimalViewModel(culture) { Species = speciesSet };

            return View(viewModel);
        }

        /// <summary>
        /// Used to obtain all the animal breeds in the context
        /// </summary>
        /// <param name="specieId">Specie ID</param>
        /// <returns>Collection with all animal breeds</returns>
        public JsonResult GetAnimalBreeds([Bind(Prefix = "id")] int? specieId)
        {
            var breedsSet = specieId != null ? _context.AnimalBreeds.Where(b => b.SpecieId == specieId) : _context.AnimalBreeds;
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            return Json(breedsSet.Select(b => new { b.BreedId, Name = culture.Name == "pt-PT" ? b.NamePt : b.Name }));
        }

        /// <summary>
        /// Used to obtain all animals by breed
        /// </summary>
        /// <param name="breedId">Breed ID</param>
        /// <returns>Collection of Animals</returns>
        public JsonResult GetAnimalsByBreed([Bind(Prefix = "id")] int? breedId)
        {
            var animalsSet = breedId != null ? _context.Animals.Where(a => a.BreedId == breedId) : _context.Animals;
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            return Json(animalsSet);
        }

        /// <summary>
        /// Used to obtain all animals by specie
        /// </summary>
        /// <param name="specieId">Specie ID</param>
        /// <returns>Collection of Animals</returns>
        public JsonResult GetAnimalsBySpecie([Bind(Prefix = "id")] int? specieId)
        {
            var animalsSet = specieId != null ? _context.Animals.Where(a => a.Breed.SpecieId == specieId) : _context.Animals;
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            return Json(animalsSet);
        }

        /// <summary>
        /// Used to create a new animal in the context
        /// </summary>
        /// <param name="animal">Animal Details</param>
        /// <param name="image">Image Data</param>
        /// <returns>View Model</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal, [Bind(Prefix = "Animal.Image")] IFormFile image)
        {
            var speciesSet = _context.AnimalSpecies;
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var viewModel = new AnimalViewModel(culture) { Species = speciesSet };
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        Account account = new Account("adotaqui", "763436643874459", "G8jgeFUttCwjs-y-aJ0vjzLkUOA");
                        Cloudinary cloudinary = new Cloudinary(account);
                        Random random = new Random();
                        var uploadParams = new ImageUploadParams()
                        {
                            Folder = "adotaqui",
                            File = new FileDescription(random.Next(10000000, 99999999).ToString(), memoryStream)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        animal.Image = uploadResult.SecureUri.ToString();
                    }
                }
                animal.Breed = _context.AnimalBreeds.FirstOrDefault(a => a.BreedId == animal.Breed.BreedId);
                _context.Add(animal);
                await _context.SaveChangesAsync();
                viewModel.StatusMessage = new StatusMessage { Type = MessageType.Success, Value = "O animal foi criado com sucesso." };
                return View(viewModel);
            }
            viewModel.StatusMessage = new StatusMessage { Type = MessageType.Error, Value = "Não foi possível criar o animal." };
            return View(viewModel);
        }

        /// <summary>
        /// Used to see the details of animal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            return await Edit(id);
        }

        /// <summary>
        /// Used to obtain the form to edit the details of animal
        /// </summary>
        /// <param name="id">Animal ID</param>
        /// <returns>Edit View</returns>
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Animal animal = await _context.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var breedsSet = _context.AnimalBreeds;
            var speciesSet = _context.AnimalSpecies;
            var adoptions = _context.AdoptionRequests.Include(a => a.User)
                                                    .Include(a => a.Animal)
                                                    .Include(a => a.AdoptionLogs)
                                                    .ThenInclude(p => p.AdoptionState);
            var commentsSet = from c in _context.AnimalComment select c;
            commentsSet = commentsSet.Where(s => s.AnimalId.Equals(id));
            foreach (AnimalComment com in commentsSet)
            {
                var user = _context.Users.Where(u => u.Id.Equals(com.UserId)).First();
                com.SetEmail(user.Email);
                com.SetUserName(user.Name);
                com.SetUserImage(user.ImageURL);
            }


            var vs = new AnimalViewModel(animal, culture, speciesSet, breedsSet, commentsSet, adoptions);
            return View(vs);
        }

        /// <summary>
        /// Used to edit the animal details
        /// </summary>
        /// <param name="id">Animal ID</param>
        /// <param name="animal">New Animal Details</param>
        /// <param name="image">New Animal Image Data</param>
        /// <returns>Index View</returns>
        [Authorize(Roles = "Administrator")]
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Weight,Height,Details")] Animal animal, [Bind(Prefix = "Animal.Image")] IFormFile image)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            var curAnimal = await _context.Animals.FindAsync(id);
            curAnimal.Name = animal.Name;
            curAnimal.Weight = animal.Weight;
            curAnimal.Height = animal.Height;
            curAnimal.Details = animal.Details;

            try
            {
                if (image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        Account account = new Account("adotaqui", "763436643874459", "G8jgeFUttCwjs-y-aJ0vjzLkUOA");
                        Cloudinary cloudinary = new Cloudinary(account);
                        Random random = new Random();
                        var uploadParams = new ImageUploadParams()
                        {
                            Folder = "adotaqui",
                            File = new FileDescription(random.Next(10000000, 99999999).ToString(), memoryStream)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        curAnimal.Image = uploadResult.SecureUri.ToString();
                    }
                }

                _context.Update(curAnimal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return await Edit(id);
        }

        /// <summary>
        /// Used to delete animal
        /// </summary>
        /// <param name="animal">Animal ID</param>
        /// <returns>Index View</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("AnimalId")] Animal animal)
        {
            var curAnimal = await _context.Animals.FindAsync(animal.AnimalId);
            if (curAnimal == null)
                return NotFound();

            _context.Remove(curAnimal);
            await _context.SaveChangesAsync();
            return Redirect(Url.Content("~/"));
        }

        /// <summary>
        /// Used to Add new commentary on animal page
        /// </summary>
        /// <param name="Commentary">Commentary</param>
        /// <param name="animal">Animal ID</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(string Commentary, [Bind("AnimalId,Commentary")] AnimalComment animal)
        {
            User currentUser = (from u in _context.Users select u).Where(s => s.Email.Equals(User.Identity.Name)).First();

            AnimalComment comment = new AnimalComment();
            comment.AnimalId = animal.AnimalId;
            comment.UserId = currentUser.Id;
            comment.Commentary = Commentary;
            comment.InsertDate = DateTime.Now;

            _context.Add(comment);
            await _context.SaveChangesAsync();

            Animal fAnimal = await _context.Animals.FindAsync(animal.AnimalId);
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var breedsSet = _context.AnimalBreeds;
            var speciesSet = _context.AnimalSpecies;

            var commentsSet = from c in _context.AnimalComment select c;
            commentsSet = commentsSet.Where(s => s.AnimalId.Equals(fAnimal.AnimalId));
            foreach (AnimalComment com in commentsSet)
            {
                var user = _context.Users.Where(u => u.Id.Equals(com.UserId)).First();
                com.SetEmail(user.Email);
                com.SetUserName(user.Name);
                com.SetUserImage(user.ImageURL);
            }

            var vs = new AnimalViewModel(fAnimal, culture, speciesSet, breedsSet, commentsSet);

            return View("Details", vs);
        }

        /// <summary>
        /// Used to remove own commentary from animal page
        /// </summary>
        /// <param name="CommentId">Commentary ID</param>
        /// <param name="animal">Animal ID</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveComment(int CommentId, [Bind("AnimalId")] AnimalComment animal)
        {
            AnimalComment commentary = (from c in _context.AnimalComment select c).Where(c => c.CommentId.Equals(CommentId)).First();
            _context.Remove(commentary);
            await _context.SaveChangesAsync();

            Animal fAnimal = await _context.Animals.FindAsync(animal.AnimalId);
            var rqf = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var breedsSet = _context.AnimalBreeds;
            var speciesSet = _context.AnimalSpecies;

            var commentsSet = from c in _context.AnimalComment select c;
            commentsSet = commentsSet.Where(s => s.AnimalId.Equals(fAnimal.AnimalId));
            foreach (AnimalComment com in commentsSet)
            {
                var user = _context.Users.Where(u => u.Id.Equals(com.UserId)).First();
                com.SetEmail(user.Email);
                com.SetUserName(user.Name);
                com.SetUserImage(user.ImageURL);
            }

            var vs = new AnimalViewModel(fAnimal, culture, speciesSet, breedsSet, commentsSet);
            return View("Details", vs);
        }



    }

}