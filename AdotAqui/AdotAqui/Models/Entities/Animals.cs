using System;
using System.Collections.Generic;

namespace AdotAqui.Models.Entities
{
    public partial class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public string Gender { get; set; }
        public int BreedId { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public AnimalBreed Breed { get; set; }
    }
}
