using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// Application ViewModels
/// </summary>
namespace AdotAqui.Models.ViewModels
{
    /// <summary>
    /// Animals ViewModel used to join all the animals, Specie and Breeds
    /// </summary>
    public class AnimalsViewModel
    {
        private CultureInfo cultureInfo;

        public IEnumerable<Animal> Animals { get; set; }
        public IEnumerable<AnimalSpecie> Species { get; set; }
        public IEnumerable<AnimalBreed> Breeds { get; set; }
        public string AnimalName { get; set; }
        public int SpecieId { get; set; }
        public int BreedId { get; set; }

        public AnimalsViewModel(CultureInfo culture)
        {
            cultureInfo = culture;
        }

        public string GetCulture()
        {
            return cultureInfo.Name;
        }

        public IEnumerable<SelectListItem> GetSpeciesDropDown()
        {
            if(cultureInfo.Name == "pt-PT") {
                return Species.OrderBy(s => s.Name).Select(s => new SelectListItem { Value = s.SpecieId.ToString(), Text = s.NamePt, Selected = SpecieId == s.SpecieId});
            } else {
                return Species.OrderBy(s => s.Name).Select(s => new SelectListItem { Value = s.SpecieId.ToString(), Text = s.Name, Selected = SpecieId == s.SpecieId });
            }
        }

        public IEnumerable<SelectListItem> GetBreedsDropDown()
        {
            if (cultureInfo.Name == "pt-PT") {
                return Breeds.OrderBy(b => b.Name).Select(b => new SelectListItem { Value = b.BreedId.ToString(), Text = b.NamePt, Selected = BreedId == b.BreedId });
            }
            else {
                return Breeds.OrderBy(b => b.Name).Select(b => new SelectListItem { Value = b.BreedId.ToString(), Text = b.Name, Selected = BreedId == b.BreedId });
            }
        }
    }
}
