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

namespace AdotAqui.Controllers
{
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
    }
}