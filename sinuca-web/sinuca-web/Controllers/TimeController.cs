using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sinuca_web.Models;
using System.Web;
using System.Net;
using System.Net.Http;

//using System.Web.Mvc;

namespace sinuca_web.Controllers
{
    public class TimeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Times = new Time().TodosOsTimes();
            return View();
        }
    }
}
