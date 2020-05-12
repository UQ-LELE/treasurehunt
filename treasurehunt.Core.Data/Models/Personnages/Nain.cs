using System;
using System.Collections.Generic;
using System.Text;
using treasurehunt.Core.Data.Models.Personnages;

namespace treasurehunt.Core.Data.Models
{
    public class Nain : Avatar
    {     
        public override string SpecialAbility()
        {
            return "Force";
        }
    }
}
