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

        [HttpPost]
        public void Criar()
        {
            Time t = new Time();

            t.Nome = Request.Form["nome"];
            t.Jogador1 = Request.Form["jogador1"];
            t.Jogador2 = Request.Form["jogador2"];
            t.Save();
            Response.Redirect("/time");
        }
        public IActionResult AdicionaTabela(int ID)
        {
            ViewBag.Tabelas = new Tabela().Tabelas();
            ViewBag.id = ID;
            return View();

        }
    }
}
