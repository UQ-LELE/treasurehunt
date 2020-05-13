using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace treasurehunt.Core.Data.Models.Quest
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Enoncé de la question
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Evenement associé à la Question
        /// </summary>
        public int StoryEventId { get; set; }

        /// <summary>
        /// Liste des choix possibles
        /// </summary>
        /// 
        public List<Choice> ChoicesEvent { get; set; }

    }
}
