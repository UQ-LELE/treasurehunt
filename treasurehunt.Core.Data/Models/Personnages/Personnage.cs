﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace treasurehunt.Core.Data.Models
{
    public abstract class Personnage
    {

        public int ID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le nom est limité à 20 charactères maximum")]
        public int Name { get; set; }

        public int Health { get; set; }

        public int Attack { get; set; }

        public abstract string SpecialAbility();

    }
}
