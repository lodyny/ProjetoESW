using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdotAqui.Models.Entities
{
    public partial class Animal
    {
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Error_NameRequired")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Error_NameLengthInvalid")]
        [Display(Prompt = "Label_Name")]
        public string Name { get; set; }

        public int? UserId { get; set; }

        [Required(ErrorMessage = "Error_GenderRequired")]
        [StringLength(1, ErrorMessage = "Error_GenderLengthInvalid")]
        public string Gender { get; set; }

        public string Species { get; }

        public int BreedId { get; set; }
      //  public int SpecieId { get; set; }

        [Required(ErrorMessage = "Error_HeightRequired")]
        public double Height { get; set; }

        [Required(ErrorMessage = "Error_WeightRequired")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Error_BirthdateRequired")]
        [DataType(DataType.DateTime)]
        [Display(Prompt = "Label_Birthday")]
        public DateTime? Birthday { get; set; }

        public string Details { get; set; }
        public string Image { get; set; }
        public AnimalBreed Breed { get; set; }
    }
}
