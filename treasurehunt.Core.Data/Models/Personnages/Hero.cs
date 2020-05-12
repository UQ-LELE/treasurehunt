using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Personnages;

namespace treasurehunt.Core.Data.Models
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
