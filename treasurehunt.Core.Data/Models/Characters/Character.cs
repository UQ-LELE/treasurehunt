using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace treasurehunt.Core.Data.Models.Characters
{

    public abstract class Character
    {
        public Guid Id { get; set; }     

        [Required(ErrorMessage ="Veuillez sélectionner un avatar")]     
        public string Race { get; set; }

        [Required]
        [Range(1, 100)]
        public int Health { get; set; }

        [Required]
        [Range(1, 100)]
        public int Attack { get; set; }
        
        [NotMapped]
        public bool IsDead { get; set; }
    }
}
