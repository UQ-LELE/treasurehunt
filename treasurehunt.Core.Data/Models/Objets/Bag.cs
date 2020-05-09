using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Objets
{
    public class Bag
    {
        public ICollection<ItemOnBag> ItemsOnBag { get; set; }
    }
}
