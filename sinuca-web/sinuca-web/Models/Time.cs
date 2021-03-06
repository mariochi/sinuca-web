using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sinuca_web.Database;

namespace sinuca_web.Models
{
    public class Time : IComparable
    {
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Jogador1 { get; set; }
        public string Jogador2 { get; set; }

        public long TabelaID { get; set; }
        public long pontos { get; set; }
        public Time()
        {
            ID = -1;
        }
        public List<Time> TodosOsTimes()
        {
            /*if(TimesDisponiveis ==  null)
            {
                TimesDisponiveis = new List<Time>();
                TimesDisponiveis.Add(new Time { ID = 1, Nome = "A", Jogador1 = "AA", Jogador2 = "BB" });
                TimesDisponiveis.Add(new Time { ID = 2, Nome = "B", Jogador1 = "AA", Jogador2 = "BB" });
                TimesDisponiveis.Add(new Time { ID = 3, Nome = "C", Jogador1 = "AA", Jogador2 = "BB" });
                TimesDisponiveis.Add(new Time { ID = 4, Nome = "D", Jogador1 = "AA", Jogador2 = "BB" });
                TimesDisponiveis.Add(new Time { ID = 5, Nome = "E", Jogador1 = "AA", Jogador2 = "BB" });
                TimesDisponiveis.Add(new Time { ID = 6, Nome = "F", Jogador1 = "AA", Jogador2 = "BB" });
                TimesDisponiveis.Add(new Time { ID = 7, Nome = "G", Jogador1 = "AA", Jogador2 = "BB" });
                counter = 8;
            }
            return TimesDisponiveis;
            */

            return DataCenter.GetTimes();
        }

        public List<Time> TimesDaTabela(long id)
        {
            List<Time> lt = TodosOsTimes().Where(t => t.TabelaID == id).ToList();
            return lt;
        }
        public void Save()
        {
            /*
            if (ID == -1)
            {
                this.ID = counter;
                counter++;
                pontos = 0;
                TodosOsTimes().Add(this);
            }
            else
            {
                Time esse = TodosOsTimes().Where(t => t.ID == this.ID).First();
                esse.Nome = Nome;
                esse.Jogador1 = Jogador1;
                esse.Jogador2 = Jogador2;
                esse.pontos = pontos;
            }
            */
            DataCenter.SaveTime(this);
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Time outroTime = obj as Time;
            if (outroTime != null)
                return pontos.CompareTo(outroTime.pontos);
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
}
