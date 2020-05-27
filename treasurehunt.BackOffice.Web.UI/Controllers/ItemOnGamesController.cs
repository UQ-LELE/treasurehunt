using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.ItemsOnGame;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class ItemOnGamesController : Controller
    {
        private readonly DalItemOnGame _dalItemOnGame;

        public ItemOnGamesController(DalItemOnGame context)
        {
            _dalItemOnGame = context;
        }

        // GET: ItemOnGames
        public async Task<IActionResult> Index()
        {
            return View(await _dalItemOnGame.GetAll());
        }

        
        // GET: ItemOnGames/Create
        public IActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemOnGame itemOnGame, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in Image)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            itemOnGame.Image = stream.ToArray();
                            await _dalItemOnGame.Add(itemOnGame);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(itemOnGame);
        }

        // GET: ItemOnGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOnGame = await _dalItemOnGame.GetById(id.Value);
            if (itemOnGame == null)
            {
                return NotFound();
            }
            return View(itemOnGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ItemOnGame itemOnGame, List<IFormFile> Image)
        {
            if (id != itemOnGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in Image)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                itemOnGame.Image = stream.ToArray();
                                await _dalItemOnGame.Edit(itemOnGame);
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemOnGameExists(itemOnGame.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemOnGame);
        }

        // GET: ItemOnGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOnGame = await _dalItemOnGame.GetById(id.Value);
            if (itemOnGame == null)
            {
                return NotFound();
            }

            return View(itemOnGame);
        }

        // POST: ItemOnGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enemy = await _dalItemOnGame.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ItemOnGameExists(int id)
        {
            return _dalItemOnGame.ItemExists(id);
        }
    }
}
