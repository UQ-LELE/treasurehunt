using System;
using System.ComponentModel.DataAnnotations;


namespace treasurehunt.Core.Data.Models.Characters
{

    public abstract class Character
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom est requis")]
        [StringLength(20, ErrorMessage = "Le nom est limité à 20 charactères maximum")]
        public string Name { get; set; }

        [Required]     
        public string Race { get; set; }

        [Required]
        [Range(1, 100)]
        public int Health { get; set; }

        [Required]
        [Range(1, 100)]
        public int Attack { get; set; }

        [Required]
        public bool IsDead { get; set; }

        [Required]
        public bool IsHero { get; set; }

        public abstract string SpecialAbility();


    }
}
