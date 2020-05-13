using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class ChoiceController : Controller
    {
        #region Champs privés
        private DalChoice _context = null;
        #endregion

        #region Constructors
        public ChoiceController(DalChoice context)
        {
            this._context = context;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create(string titleOfEvent, string numberOfEvent)
        {
            ViewBag.EventForChoice = JsonConvert.DeserializeObject<StoryEvent>(TempData["EventFromBoard"].ToString());
            ViewBag.ListOfEvents = JsonConvert.DeserializeObject<List<StoryEvent>>(TempData["ListOfEvents"].ToString());

            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Choice choiceForQuestion)
        {
            IActionResult result = this.View(choiceForQuestion);

            if (ModelState.IsValid)
            {
                this._context.AddChoice(choiceForQuestion);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = choiceForQuestion.StoryEventId });
            }

            return result;
        }

        public IActionResult Edit(int id)
        {
            Choice choiceToEdit = this._context.GetChoiceById(id);

            ViewBag.EventForChoice = JsonConvert.DeserializeObject<StoryEvent>(TempData["EventFromBoard"].ToString());
            ViewBag.ListOfEvents = JsonConvert.DeserializeObject<List<StoryEvent>>(TempData["ListOfEvents"].ToString());

            if (choiceToEdit == null)
            {
                return NotFound();
            }

            return View(choiceToEdit);
        }

        [HttpPost]
        public IActionResult EditPost(Choice choiceToEdit)
        {
            IActionResult result = this.View(choiceToEdit);

            if (ModelState.IsValid)
            {
                this._context.EditChoice(choiceToEdit);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = choiceToEdit.StoryEventId });
            }
            return result;
        }
    }
}