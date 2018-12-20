using AdotAqui.Models.Entities;
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
        //public IEnumerable<AnimalSpecie> Species { get; set; }
        public IEnumerable<AnimalBreed> Breeds { get; set; }

        public AnimalViewModel(CultureInfo culture)
        {
            cultureInfo = culture;
        }

        /*public IEnumerable<SelectListItem> GetSpeciesDropDown()
        {
            if (cultureInfo.Name == "pt-PT")
            {
                return Species.OrderBy(s => s.Name).Select(s => new SelectListItem { Value = s.SpecieId.ToString(), Text = s.NamePt, Selected = SpecieId == s.SpecieId });
            }
            else
            {
                return Species.OrderBy(s => s.Name).Select(s => new SelectListItem { Value = s.SpecieId.ToString(), Text = s.Name, Selected = SpecieId == s.SpecieId });
            }
        }*/

        public IEnumerable<SelectListItem> GetBreedsDropDown()
        {
            if (cultureInfo.Name == "pt-PT")
            {
                return Breeds.OrderBy(b => b.Name).Select(b => new SelectListItem { Value = b.BreedId.ToString(), Text = b.NamePt});
            }
            else
            {
                return Breeds.OrderBy(b => b.Name).Select(b => new SelectListItem { Value = b.BreedId.ToString(), Text = b.Name});
            }
        }
    }
}
