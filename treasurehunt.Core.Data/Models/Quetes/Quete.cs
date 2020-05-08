﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace treasurehunt.Core.Data.Models.Quetes
{
    public class Quete
    {
        #region Propriétés

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le titre est requis")]
        public string Titre { get; set; }

        #endregion
    }
}