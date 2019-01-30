using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.ViewModels
{
    public class AnimalViewModel
    {

        [TempData]
        public StatusMessage StatusMessage { get; set; }

        private CultureInfo cultureInfo;
        public Animal Animal { get; set; }
        public IEnumerable<AnimalSpecie> Species { get; set; }
        public IEnumerable<AnimalBreed> Breeds { get; set; }
        public IEnumerable<AnimalComment> Comments { get; set; }
        public IEnumerable<AdoptionRequests> Adoptions { get; set; }

        public AnimalViewModel()
        {
        }

        public AnimalViewModel(Animal animal)
        {
            Animal = animal;
        }

        public AnimalViewModel(Animal animal, CultureInfo culture, IEnumerable<AnimalSpecie> species, IEnumerable<AnimalBreed> breeds, IEnumerable<AnimalComment> comments)
        {
            Animal = animal;
            cultureInfo = culture;
            Species = species;
            Breeds = breeds;
            Comments = comments;
        }

        public AnimalViewModel(Animal animal, CultureInfo culture, IEnumerable<AnimalSpecie> species, IEnumerable<AnimalBreed> breeds, IEnumerable<AnimalComment> comments, IEnumerable<AdoptionRequests> adoptions)
        {
            Animal = animal;
            cultureInfo = culture;
            Species = species;
            Breeds = breeds;
            Comments = comments;
            Adoptions = adoptions;
        }

        public AnimalViewModel(CultureInfo culture)
        {
            cultureInfo = culture;
        }

       public IEnumerable<SelectListItem> GetSpeciesDropDown()
        {
            if (cultureInfo.Name == "pt-PT") {
                return Species.OrderBy(s => s.Name).Select(s => new SelectListItem { Value = s.SpecieId.ToString(), Text = s.NamePt});
            }
            else {
                return Species.OrderBy(s => s.Name).Select(s => new SelectListItem { Value = s.SpecieId.ToString(), Text = s.Name });
            }
        }

        public IEnumerable<SelectListItem> GetBreedsDropDown()
        {
            if (cultureInfo.Name == "pt-PT") {
                return Breeds.OrderBy(b => b.Name).Select(b => new SelectListItem { Value = b.BreedId.ToString(), Text = b.NamePt});
            }
            else {
                return Breeds.OrderBy(b => b.Name).Select(b => new SelectListItem { Value = b.BreedId.ToString(), Text = b.Name});
            }
        }

        public string GetCulture()
        {
            return cultureInfo.Name;
        }
    }
}
