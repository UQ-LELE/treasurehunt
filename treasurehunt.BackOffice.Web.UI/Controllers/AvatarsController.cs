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
    public class AvatarsController : Controller
    {
        private readonly DalAvatar _dalAvatar;

        public AvatarsController(DalAvatar context)
        {
            _dalAvatar = context;
        }

        // GET: Avatars
        public async Task<IActionResult> Index()
        {
            return View(await _dalAvatar.GetAll());
        }
      

        // GET: Avatars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avatars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Race,Health,Attack,Image")] Avatar avatar, List<IFormFile> Image)
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
                            avatar.Image = stream.ToArray();
                            await this._dalAvatar.Add(avatar);
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }    
            }
            return View(avatar);
        }

        // GET: Avatars/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var avatar = await _dalAvatar.GetById(id);
            if (avatar == null)
            {
                return NotFound();
            }
            return View(avatar);
        }

        // POST: Avatars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Race,Health,Attack,Image")] Avatar avatar, List<IFormFile> Image)
        {
            if (id != avatar.Id)
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
                                avatar.Image = stream.ToArray();
                                await _dalAvatar.Edit(avatar);
                            }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dalAvatar.AvatarExists(id))
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
            return View(avatar);
        }

        // GET: Avatars/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _dalAvatar.GetById(id);
            if (avatar == null)
            {
                return NotFound();
            }

            return View(avatar);
        }

        // POST: Avatars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avatar = await _dalAvatar.DeleteById(id);
           
            return RedirectToAction(nameof(Index));
        }

    }
}
