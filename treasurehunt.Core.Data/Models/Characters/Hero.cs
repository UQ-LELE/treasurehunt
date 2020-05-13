using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using treasurehunt.Core.Data.Models.ItemsOnGame;

namespace treasurehunt.Core.Data.Models.Characters
{
    [Table("Heroes")]
    public class Hero : Character
    {
        public bool IsPoisoned { get; set; }

        [NotMapped]
        public List<ItemOnGame> ItemsOnBag { get; set; }

        [NotMapped]
        public List<string> HisPath { get; set; }

        [NotMapped]
        public List<string> HisChoices { get; set; }

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

        public override string SpecialAbility()
        {
            throw new NotImplementedException();
        }
    }
}
