using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolOA.Controllers
{
    public class SchoolMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlanTable()
        {
            return View();
        }

        public IActionResult PlanDetail(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}