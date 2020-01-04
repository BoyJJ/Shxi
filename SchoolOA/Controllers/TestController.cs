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

        public IActionResult WageDetail(string teacherid, string time)
        {
            ViewData["teacherid"] = teacherid;
            ViewData["time"] = time;
            return View();
        }


    }
}