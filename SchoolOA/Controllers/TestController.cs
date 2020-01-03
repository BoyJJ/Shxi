using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SchoolOA.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestTable() 
        {
            return View();
        }

        public IActionResult TestSelectTime() {
            return View();
        }

        public IActionResult WageDetial(string teacherid, string time)
        {
            ViewData["teacherid"] = teacherid;
            ViewData["time"] = time;
            return View();
        }

        public IActionResult EditWage(string teacherid, string time)
        {
            ViewData["teacherid"] = teacherid;
            ViewData["time"] = time;
            return View();
        }
    }
}