using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace sinuca_web.Models
{
    public class Time
    {
        private static int counter;
        private static List<Time> TimesDisponiveis { get; set; }
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Jogador1 { get; set; }
        public string Jogador2 { get; set; }

        public Time()
        {
            ID = -1;
        }
        public List<Time> TodosOsTimes()
        {
            if(TimesDisponiveis ==  null)
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
        }

        public void Save()
        {
            //TO DO
            if (ID == -1)
            {
                this.ID = counter;
                counter++;
                TodosOsTimes().Add(this);
            }
            else
            {
                Time esse = TodosOsTimes().Where(t => t.ID == this.ID).First();
                esse.Nome = Nome;
                esse.Jogador1 = Jogador1;
                esse.Jogador2 = Jogador2;
            }
            
        }
    }
}
