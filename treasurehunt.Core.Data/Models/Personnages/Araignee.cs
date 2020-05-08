using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Personnages
{
    public class Araignee : Personnage
    {
        public override string SpecialAbility()
        {
            return "Morsure empoisonnée";
        }
    }
}
