using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using treasurehunt.Core.Data.Models.Quetes;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class QueteController : Controller
    {    
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
            

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Quete quete)
        {
            return View();
        }


    }
}