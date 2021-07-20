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
        public const int MAX_TIMES_POR_TABELA = 10;
        public int ID { get; set; }
        public string Nome { get; set; }
        public string PremiacaoDescrição { get; set; }
        public int Pontuacao { get; set; }
        public string RegraDescricao { get; set; }


        public Tabela()
        {
            ID = -1;
        }

        public List<Time> MeusTimes()
        {
            return new Time().TimesDaTabela(ID);

        }
        public List<Tabela> Tabelas()
        {
            if(tabelas == null)
            {
                tabelas = new List<Tabela>();
                tabelas.Add(new Tabela { ID = 1, Nome = "Time Teste", PremiacaoDescrição = "Uma vaga de emprego", Pontuacao = 200, RegraDescricao = "Não Sei" });
                counter = 2;
            }
            return tabelas;
        }
        public Tabela PorID(int ID)
        {
            Tabela t = new Tabela().Tabelas().Where(tab => tab.ID == ID).First();
            return t;
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
