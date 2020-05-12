using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Quetes;

namespace treasurehunt.Core.Data.Models.Personnages
{

    public abstract class Personnage
    {
        public Guid ID { get; set; }

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
