using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sinuca_web.Models
{
    public class Tabela
    {
        private static List<Tabela> tabelas;
        private static int counter;
        private const int MAX_TIMES_POR_TABELA = 10;
        public int ID { get; set; }
        public string Nome { get; set; }
        public string PremiacaoDescrição { get; set; }
        public int Pontuacao { get; set; }
        public string RegraDescricao { get; set; }


        public Tabela()
        {
            ID = -1;
        }
        public List<Tabela> Tabelas()
        {
            if(tabelas == null)
            {
                tabelas = new List<Tabela>();
                counter = 1;
            }
            return tabelas;
        }

        public void Save()
        {
            if(ID == -1)
            {
                ID = counter;
                counter++;
                Tabelas().Add(this);
            }
            else
            {
                Tabela tab = Tabelas().Where(t => t.ID == ID).First();
                tab.Nome = Nome;
                tab.PremiacaoDescrição = PremiacaoDescrição;
                tab.Pontuacao = Pontuacao;
                tab.RegraDescricao = RegraDescricao;
            }
        }
    }
}
