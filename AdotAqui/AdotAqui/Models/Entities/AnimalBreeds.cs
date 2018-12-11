using System;
using System.Collections.Generic;

namespace AdotAqui.Models.Entities
{
    public partial class AnimalBreed
    {
        public AnimalBreed()
        {
            Animals = new HashSet<Animal>();
        }

        public int BreedId { get; set; }
        public string Name { get; set; }
        public string NamePt { get; set; }
        public int SpecieId { get; set; }

        public AnimalSpecie Specie { get; set; }
        public ICollection<Animal> Animals { get; set; }
    }
}
