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
        private DalChoice _dalChoice = null;
        #endregion

        #region Constructors
        public ChoiceController(DalChoice context)
        {
            this._dalChoice = context;
        }
        #endregion

        public async Task<IActionResult> Create([FromServices] DalStoryEvent dalStoryEvent, int idStoryEvent)
        {
            ViewBag.EventForChoice = await dalStoryEvent.GetById(idStoryEvent);
            ViewBag.ListOfEvents = await dalStoryEvent.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Choice choiceForQuestion)
        {
            IActionResult result = this.View(choiceForQuestion);

            if (ModelState.IsValid)
            {
                await this._dalChoice.Add(choiceForQuestion);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = choiceForQuestion.StoryEventId });
            }

            return result;
        }

        public async Task<IActionResult> Edit([FromServices] DalStoryEvent dalStoryEvent, int idChoice, int idStoryEvent)
        {
            Choice choiceToEdit = await this._dalChoice.GetById(idChoice);

            ViewBag.EventForChoice = await dalStoryEvent.GetById(idStoryEvent);
            ViewBag.ListOfEvents = await dalStoryEvent.GetAll();

            if (choiceToEdit == null)
            {
                return NotFound();
            }

            return View(choiceToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Choice choiceToEdit)
        {
            IActionResult result = this.View(choiceToEdit);

            if (ModelState.IsValid)
            {
                await this._dalChoice.Edit(choiceToEdit);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = choiceToEdit.StoryEventId });
            }
            return result;
        }

        // GET: Enemies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var enemy = await _dalChoice.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }

            return View(enemy);
        }

        // POST: Enemies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enemy = await _dalChoice.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
