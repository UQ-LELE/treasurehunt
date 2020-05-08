using System;
using System.Collections.Generic;
using System.Text;
using treasurehunt.Core.Data.Models.Quetes;

namespace treasurehunt.Core.Data.DataLayer
{
    /// <summary>
    /// Layer d'accès à la base de données (encapsule l'appel entities)
    /// </summary>
    public class ChoixDataLayer
    {
        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public ChoixDataLayer(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Ajoute et sauvegarde une nouvelle aventure
        /// </summary>
        /// <param name="evenement"></param>
        public void Add(Choix choix)
        {
            this._context.Choixes.Add(choix);
            this._context.SaveChanges();
        }

        public void Edit(Choix choix)
        {
            //edit linq
            this._context.SaveChanges();
        }
        #endregion
    }
}
