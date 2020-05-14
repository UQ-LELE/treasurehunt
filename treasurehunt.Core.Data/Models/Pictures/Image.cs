using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Pictures
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
