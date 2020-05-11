using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Personnages;
using treasurehunt.Core.Data.Models.Quetes;
using treasurehunt.Web.UI.Commons;
using treasurehunt.Web.UI.Utilities;

namespace treasurehunt.Web.UI.Controllers
{
    public class GameController : Controller
    {
        #region vars
        private Rat rat;
        private Dragon dragon;
        private Spider spider;
        private Bear bear;
        private Personnage hero;
        private Evenement evenement;
        private int i = 0;
        #endregion

        #region Champs privés
        private DefaultContext _context = null;
        #endregion

        #region Constructeurs
        public GameController(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        public IActionResult Index(string choice)
        {
            evenement = new Evenement();

            hero = HttpContext.Session.GetComplexObject<Personnage>("Hero");
            rat = HttpContext.Session.GetComplexObject<Rat>("Rat");
            spider = HttpContext.Session.GetComplexObject<Spider>("Spider");
            bear = HttpContext.Session.GetComplexObject<Bear>("Bear");
            dragon = HttpContext.Session.GetComplexObject<Dragon>("Dragon");

            ViewBag.Evenement = getNextEventFromDatabase(choice);
            ViewBag.Question = getQuestionFromDatabase(evenement);
            ViewBag.Choix = getChoixesFromDatabase(evenement);
            i++;
            return View();
        }

        private Evenement getNextEventFromDatabase(string choice)
        {
            return evenement = this._context.Evenements.First(item => item.Numero == choice);
        }

        private List<Choix> getChoixesFromDatabase(Evenement evenement)
        {
            List<Choix> choixesForThisEvent = this._context.Choixes.Where(choix => choix.EventNumber == evenement.Numero).ToList();
            return choixesForThisEvent;
        }

        private Question getQuestionFromDatabase(Evenement evenement)
        {
            Question questionForThisEvent = this._context.Questions.First(question => question.ID == evenement.QuestionId);
            return questionForThisEvent;
        }

    }
}