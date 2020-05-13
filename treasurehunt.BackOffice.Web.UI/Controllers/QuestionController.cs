using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class QuestionController : Controller
    {
        #region Champs privés
        private DalQuestion _context = null;
        #endregion

        #region Constructors
        public QuestionController(DalQuestion context)
        {
            this._context = context;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(StoryEvent eventForQuestion)
        {
            ViewBag.EventForQuestion = eventForQuestion;

            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Question questionForEvent)
        {
            IActionResult result = this.View(questionForEvent);

            if (ModelState.IsValid)
            {
                this._context.AddQuestion(questionForEvent);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = questionForEvent.StoryEventId });
            }

            return result;
        }

        public IActionResult Edit(int id)
        {
            Question questionToEdit = this._context.GetQuestionById(id);

            ViewBag.EventForQuestion = JsonConvert.DeserializeObject<StoryEvent>(TempData["EventFromBoard"].ToString());

            if (questionToEdit == null)
            {
                return NotFound();
            }

            return View(questionToEdit);
        }

        [HttpPost]
        public IActionResult EditPost(Question questionToEdit)
        {
            IActionResult result = this.View(questionToEdit);

            if (ModelState.IsValid)
            {
                this._context.EditQuestion(questionToEdit);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = questionToEdit.StoryEventId });
            }
            return result;
        }
    }
}