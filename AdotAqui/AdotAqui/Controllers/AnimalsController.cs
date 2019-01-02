using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using AdotAqui.Models.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AdotAqui.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AnimalsController : Controller
    {
        private readonly AdotAquiDbContext _context;

        public AnimalsController(AdotAquiDbContext context)
        {
            _context = context;
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var breedsSet = _context.AnimalBreeds;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var viewModel = new AnimalViewModel(culture) { Breeds = breedsSet };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal, [Bind(Prefix = "Animal.Image")] IFormFile image)
        {
            var breedsSet = _context.AnimalBreeds;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            var viewModel = new AnimalViewModel(culture) { Breeds = breedsSet};
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
                _context.Add(animal);
                await _context.SaveChangesAsync();
                viewModel.StatusMessage = new StatusMessage { Type = MessageType.Success, Value = "O animal foi criado com sucesso."};
                return View(viewModel);
            }
            viewModel.StatusMessage = new StatusMessage { Type = MessageType.Error, Value = "Não foi possível criar o animal." };
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            return await Edit(id);
        }

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

            var commentsSet = from c in _context.AnimalComment select c;
            commentsSet = commentsSet.Where(s => s.AnimalId.Equals(id));
            foreach (AnimalComment com in commentsSet)
            {
                var user = _context.Users.Where(u => u.Id.Equals(com.UserId)).First();
                com.SetEmail(user.Email);
                com.SetUserName(user.Name);
            }

            var vs = new AnimalViewModel(animal, culture, speciesSet, breedsSet, commentsSet);
            return View(vs);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Weight,Height,Details")] Animal animal)
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
                    _context.Update(curAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
            return await Edit(id);
        }

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
            }

            var vs = new AnimalViewModel(fAnimal, culture, speciesSet, breedsSet, commentsSet);
            return View("Details", vs);
        }



    }

}