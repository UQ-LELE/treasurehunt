namespace treasurehunt.Core.Data.Models.ItemsOnGame
{
    public class ItemOnGame
    {
        /// <summary>
        ///  Id de l'item on game
        /// </summary>
        public int Id { get; set; }      

        /// <summary>
        /// Nom de l'item on game
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description de l'item on game
        /// </summary>
        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}
