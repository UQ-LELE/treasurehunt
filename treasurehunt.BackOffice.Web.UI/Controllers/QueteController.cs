using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.Models.Quetes;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class QueteController : Controller
    {
        #region Champs privés
        private DefaultContext _context = null;

        #endregion

        #region Constructeurs
        public QueteController(DefaultContext context)
        {
            this._context = context;
        }
        #endregion
        public ActionResult Index()
        {
            List<StoryEvent> evenements = this._context.Evenements.ToList();

            return View(evenements);

        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Quete quete)
        {
            return View();

        }

        public ActionResult Edit(int id)
        {
            StoryEvent evenement = this._context.Evenements.First(evenement => evenement.Id == id) ;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Quete quete)
        {
            return View();
        }


    }
}