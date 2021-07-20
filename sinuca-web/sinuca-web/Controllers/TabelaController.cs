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

        public IActionResult Mostrar(int ID)
        {
            Tabela t = new Tabela().PorID(ID);
            ViewBag.Tabela = t;
            return View();

        }
        public IActionResult NovoTime(int id)
        {
            ViewBag.TabelaID = id;
            return View();
        }
        [HttpPost]
        public void CriarTime()
        {
            Tabela tb = new Tabela().PorID(int.Parse(Request.Form["tabelaid"]));
            if(tb.MeusTimes().Count >= Tabela.MAX_TIMES_POR_TABELA)
            {
                ViewBag.Message = "Tabela cheia";
                Response.Redirect("mostrar/?id=" + tb.ID);
            }
            Time t = new Time();
            t.Nome = Request.Form["nome"];
            t.Jogador1 = Request.Form["jogador1"];
            t.Jogador2 = Request.Form["jogador2"];
            t.TabelaID = tb.ID;
            t.Save();
            Response.Redirect("mostrar/?id="+t.TabelaID);
        }
    }
}
