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
using treasurehunt.Core.Data.Models.Characters;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class EnemiesController : Controller
    {
        private readonly DalEnemy _dalEnemy;

        public EnemiesController(DalEnemy context)
        {
            _dalEnemy = context;
        }

        // GET: Enemies
        public async Task<IActionResult> Index()
        {
            return View(await _dalEnemy.GetAll());
        }

        // GET: Enemies/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _dalEnemy.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }

            return View(enemy);
        }

        // GET: Enemies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enemies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enemy enemy, List<IFormFile> Image)
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
                            enemy.Image = stream.ToArray();
                            await _dalEnemy.Add(enemy);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(enemy);
        }

        // GET: Enemies/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _dalEnemy.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }
            return View(enemy);
        }

        // POST: Enemies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Race,Health,Attack,IsDead,IsHero,ImageId")] Enemy enemy, List<IFormFile> Image)
        {
            if (id != enemy.Id)
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
                                enemy.Image = stream.ToArray();
                                await _dalEnemy.Edit(enemy);
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnemyExists(enemy.Id))
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
            return View(enemy);
        }

        // GET: Enemies/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _dalEnemy.GetById(id);
            if (enemy == null)
            {
                return NotFound();
            }

            return View(enemy);
        }

        // POST: Enemies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var enemy = await _dalEnemy.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EnemyExists(Guid id)
        {
            return _dalEnemy.EnemyExists(id);
        }
    }
}
