using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Quetes
{
    public class Choix
    {

        public int ID { get; set; }
        /// <summary>
        /// Id de la question liée au choix
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Id de l'évènement suivant
        /// </summary>
        public int? EvenementId { get; set; }
    }
}
