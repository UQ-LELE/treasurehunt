using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class EnemyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}