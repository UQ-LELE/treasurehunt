using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Objets
{
    public abstract class ItemOnBag
    {
        public int ID { get; set; }

      
        public int Name { get; set; }

        public abstract string SpecialEffect();
    }
}
