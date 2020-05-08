using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace treasurehunt.Core.Data.Models.Quetes
{
    public class Question
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Enoncé de la question
        /// </summary>
        public string LaQuestion { get; set; }

        /// <summary>
        /// Liste des choix possible à la question
        /// </summary>
        public List<Choix> LesChoix { get; set; }
    }
}
