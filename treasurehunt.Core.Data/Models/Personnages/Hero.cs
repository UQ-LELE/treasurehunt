using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using treasurehunt.Core.Data.Models.Objets;

namespace treasurehunt.Core.Data.Models.Personnages
{
    [Table("Heroes")]
    public class Hero : Personnage
    {
        public Guid ID { get; set; }

        [NotMapped]
        public List<ItemOnBag> ItemsOnBag { get; set; }
       
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
            this.ItemsOnBag = new List<ItemOnBag>();
            this.HisPath = new List<string>();
            this.HisChoices = new List<string>();
        }

        public override string SpecialAbility()
        {
            throw new NotImplementedException();
        }
    }
}
