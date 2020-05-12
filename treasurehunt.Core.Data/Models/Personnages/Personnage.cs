using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Quetes;

namespace treasurehunt.Core.Data.Models
{

    public abstract class Personnage
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le nom est limité à 20 charactères maximum")]
        public string Name { get; set; }

        public string Race { get; set; }

        public int Health { get; set; }

        public int Attack { get; set; }

        public bool IsDead { get; set; }
        public bool IsHero { get; set; }
        public abstract string SpecialAbility();


    }
}
