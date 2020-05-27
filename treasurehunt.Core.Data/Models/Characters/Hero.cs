using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using treasurehunt.Core.Data.Models.ItemsOnGame;

namespace treasurehunt.Core.Data.Models.Characters
{
    [Table("Heroes")]
    public class Hero : Character
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom est requis")]
        [StringLength(20, ErrorMessage = "Le nom est limité à 6 charactères maximum")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsPoisoned { get; set; }

        public Hero() { }
        public Hero(string name, int health, int attack, string race)
        {
            this.Name = name;
            this.Health = health;
            this.Attack = attack;
            this.Race = race;
            this.ItemsOnBag = new List<ItemOnGame>();
            this.HisPath = new List<string>();
            this.HisChoices = new List<string>();
        }

        [NotMapped]
        public List<ItemOnGame> ItemsOnBag { get; set; }

        [NotMapped]
        public List<string> HisPath { get; set; }

        [NotMapped]
        public List<string> HisChoices { get; set; }
    }
}
