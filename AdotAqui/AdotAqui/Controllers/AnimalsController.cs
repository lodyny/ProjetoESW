using AdotAqui.Data;
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

namespace AdotAqui.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AdotAquiDbContext _context;

        public AnimalsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var speciesSet = _context.AnimalSpecies;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var viewModel = new AnimalViewModel(culture) { Species = speciesSet };

            return View(viewModel);
        }

        public JsonResult GetAnimalBreeds([Bind(Prefix = "id")] int? specieId)
        {
            var breedsSet = specieId != null ? _context.AnimalBreeds.Where(b => b.SpecieId == specieId) : _context.AnimalBreeds;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            return Json(breedsSet.Select(b => new { b.BreedId, Name = culture.Name == "pt-PT" ? b.NamePt : b.Name }));
        }

        public JsonResult GetAnimalsByBreed([Bind(Prefix = "id")] int? breedId)
        {
            var animalsSet = breedId != null ? _context.Animals.Where(a => a.BreedId == breedId) : _context.Animals;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            return Json(animalsSet);
        }

        public JsonResult GetAnimalsBySpecie([Bind(Prefix = "id")] int? specieId)
        {
            var animalsSet = specieId != null ? _context.Animals.Where(a => a.Breed.SpecieId == specieId) : _context.Animals;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            return Json(animalsSet);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal, [Bind(Prefix = "Animal.Image")] IFormFile image)
        {
            var speciesSet = _context.AnimalSpecies;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
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

        public async Task<IActionResult> Details(int? id)
        {
            return await Edit(id);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Animal animal = await _context.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
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
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveComment(int CommentId, [Bind("AnimalId")] AnimalComment animal)
        {
            AnimalComment commentary = (from c in _context.AnimalComment select c).Where(c => c.CommentId.Equals(CommentId)).First();
            _context.Remove(commentary);
            await _context.SaveChangesAsync();

            Animal fAnimal = await _context.Animals.FindAsync(animal.AnimalId);
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
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