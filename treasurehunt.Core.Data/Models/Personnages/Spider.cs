﻿using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Personnages
{
    public class Spider : Enemy
    {
        public override string SpecialAbility()
        {
            return "Morsure empoisonnée";
        }
    }
}
