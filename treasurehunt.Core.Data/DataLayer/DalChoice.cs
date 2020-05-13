using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.Core.Data.DataLayer
{
    /// <summary>
    /// Layer d'accès à la base de données (encapsule l'appel entities)
    /// </summary>
    public class DalChoice
    {
        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalChoice(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Ajoute et sauvegarde une nouvelle aventure
        /// </summary>
        /// <param name="evenement"></param>
        public void Add(Choice choix)
        {
            this._context.Choices.Add(choix);
            this._context.SaveChanges();
        }

        public void Edit(Choice choix)
        {
            //edit linq
            this._context.SaveChanges();
        }
        #endregion
    }
}
