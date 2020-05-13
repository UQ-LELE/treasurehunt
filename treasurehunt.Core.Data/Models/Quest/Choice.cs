using System.ComponentModel.DataAnnotations;

namespace treasurehunt.Core.Data.Models.Quest
{
    public class Choice
    {

        /// <summary>
        /// Id de la réponse
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Enonncé du choix
        /// </summary>
       [Required(AllowEmptyStrings = false, ErrorMessage ="Question is required")]
        public string Description { get; set; }

        /// <summary>
        /// Id de la question associé au choix
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Id du paragraphe suivant
        /// </summary>
        public int? StoryEventId { get; set; }
    }
}
