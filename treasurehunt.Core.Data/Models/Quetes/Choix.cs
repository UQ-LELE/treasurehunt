using System;
using System.Collections.Generic;
using System.Text;

namespace treasurehunt.Core.Data.Models.Quetes
{
    public class Choix
    {

        public int ID { get; set; }
        /// <summary>
        /// Id du choix
        /// </summary>
        public int ActionChoixId { get; set; }

        /// <summary>
        /// Numero de l'évènement attaché
        /// </summary>
        public string EventNumber { get; set; }

        /// <summary>
        /// Id de l'évènement suivant
        /// </summary>
        public string NexEventNumber { get; set; }
    }
}
