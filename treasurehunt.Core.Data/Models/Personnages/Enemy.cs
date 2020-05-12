using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Personnages
{
    public class Enemy : Personnage
    {
        public Guid ID { get; set; }

        public override string SpecialAbility()
        {
            throw new NotImplementedException();
        }
    }
}
