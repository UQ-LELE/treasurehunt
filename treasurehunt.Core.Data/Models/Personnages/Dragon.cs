using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Personnages
{
    public class Dragon : Enemy
    {
        public override string SpecialAbility()
        {
            return "Flamme de l'enfer";
        }
    }
}
