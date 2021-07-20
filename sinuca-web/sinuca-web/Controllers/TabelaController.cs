using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sinuca_web.Models;

namespace sinuca_web.Controllers
{
    public class TabelaController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Tabelas = new Tabela().Tabelas();
            return View();
        }
        public IActionResult Nova()
        {
            return View();
        }

        [HttpPost]
        public void Criar()
        {
            Tabela t = new Tabela();
            t.Nome = Request.Form["nome"];
            t.PremiacaoDescrição = Request.Form["premiacao"];
            t.Pontuacao = int.Parse(Request.Form["pontuacao"]);
            t.RegraDescricao = Request.Form["regra"];
            t.Save();
            Response.Redirect("/tabela");
        }
    }
}
