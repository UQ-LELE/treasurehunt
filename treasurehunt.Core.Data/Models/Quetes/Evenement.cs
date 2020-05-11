using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace treasurehunt.Core.Data.Models.Quetes
{
    public class Evenement
    {
        #region Propriétés
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Numéro de l'évènement
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Titre de l'évènement
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Titre de l'évènement requis")]
        public string Titre { get; set; }

        /// <summary>
        /// Description de l'évènement
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description de l'évènement requis")]
        public string Description { get; set; }

        /// <summary>
        /// Indique si c'est l'évènement de démarrage
        /// </summary>
        public bool EstInitial { get; set; }

        /// <summary>
        /// Question de l'évènement
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Liste des choix possibles
        /// </summary>
       // public IEnumerable<Choix> LesChoix { get; set; }
        #endregion

    }
}
