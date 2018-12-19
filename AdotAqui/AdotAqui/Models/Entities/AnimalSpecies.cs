using System;
using System.Collections.Generic;

namespace AdotAqui.Models.Entities
{
    public partial class AnimalSpecie
    {
        public AnimalSpecie()
        {
            AnimalBreeds = new HashSet<AnimalBreed>();
        }

        public int SpecieId { get; set; }
        public string Name { get; set; }
        public string NamePt { get; set; }

        public ICollection<AnimalBreed> AnimalBreeds { get; set; }
    }
}
