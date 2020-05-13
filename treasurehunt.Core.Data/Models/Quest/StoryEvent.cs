using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace treasurehunt.Core.Data.Models.Quest
{
    public class StoryEvent
    {
        #region Propriétés
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Numéro de l'évènement
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Titre de l'évènement
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Titre de l'évènement requis")]
        public string Title { get; set; }

        /// <summary>
        /// Description de l'évènement
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description de l'évènement requis")]
        public string Description { get; set; }

        /// <summary>
        /// Indique si c'est l'évènement de démarrage
        /// </summary>
        public bool IsFirstEvent { get; set; }

        /// <summary>
        /// Question du paragraphe
        /// </summary>
        public Question QuestionEvent { get; set; }

        /// <summary>
        /// Liste des réponses possibles
        /// </summary>
        public IEnumerable<Choice> ChoicesEvent { get; set; }
        #endregion

    }
}
