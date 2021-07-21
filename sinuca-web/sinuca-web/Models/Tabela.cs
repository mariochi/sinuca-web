using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using sinuca_web.Database;

namespace sinuca_web.Models
{
    public class Tabela
    {
        public const int MAX_TIMES_POR_TABELA = 10;
        public long ID { get; set; }
        public string Nome { get; set; }
        public string PremiacaoDescrição { get; set; }
        public long Pontuacao { get; set; }
        public string RegraDescricao { get; set; }


        public Tabela()
        {
            ID = -1;
        }
        public List<Time> MeusTimes()
        {
            return new Time().TimesDaTabela(ID);
        }
        public bool Cheia()
        {
            return MeusTimes().Count >= 10;
        }
        public List<Tabela> Tabelas()
        {
            return DataCenter.GetTabelas();
        }
        public Tabela PorID(long ID)
        {
            Tabela t = DataCenter.TabelaPorId(ID);
            return t;
        }
        public void Save()
        {
            DataCenter.SaveTabela(this);
        }
    }
}
