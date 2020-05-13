using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using treasurehunt.Core.Data.DataLayer;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class HeroController : Controller
    {
        #region Champs privés
        private DalHero _context = null;
        #endregion

        #region Constructors
        public HeroController(DalHero context)
        {
            this._context = context;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}