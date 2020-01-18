using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolOA.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PersonalWage()
        {
            return View();
        }

        public IActionResult SectionWage()
        {
            return View();
        }

        public IActionResult EditWage(string teacherid, string time)
        {
            ViewData["teacherid"] = teacherid;
            ViewData["time"] = time;
            return View();
        }


        public IActionResult MaterialTable()
        {
            return View();
        }

        public IActionResult MaterialDetail(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}