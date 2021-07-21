using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using sinuca_web.Models;

namespace sinuca_web.Database
{
    public class DataCenter
    {
        private const string DB_PATH = "MeuBanco.sqlite";
        private static SQLiteConnection db_connection;
        public static void CriarBanco()
        {
            
            try
            {
                SQLiteConnection.CreateFile(DB_PATH);
            }
            catch
            {
                throw;
            }
        }

        private static SQLiteConnection SQLConnection()
        {
            if(!File.Exists(DB_PATH))
            {
                CriarBanco();

            }
            if(db_connection == null)
            {
                db_connection = new SQLiteConnection("DataSource ="+DB_PATH+"; Version=3");
                db_connection.Open();
                using (var cmd = db_connection.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Tabelas(id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                      "nome TEXT," +
                                      " premiacao TEXT," +
                                      " pontuacao int, regraDescricao TEXT)";
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = db_connection.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Times(id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                      " Nome TEXT," +
                                      " jogador1 TEXT," +
                                      " jogador2 TEXT," +
                                      " pontos INTEGER, " +
                                      "tabelaid integer, "+
                                      " FOREIGN KEY (tabelaid) REFERENCES Tabelas(id))";
                    cmd.ExecuteNonQuery();
                }
                db_connection.Close();
            }
            
            return db_connection;
        }

        public static List<Tabela> GetTabelas()
        {
            try
            {
                SQLConnection().Open();
                DataTable tb = new DataTable();
                using (var cmd = SQLConnection().CreateCommand())
                {
                    cmd.CommandText = "select * from tabelas";
                    var da = new SQLiteDataAdapter(cmd.CommandText, SQLConnection());
                    da.Fill(tb);
                }

                List<Tabela> returnValue = tb.AsEnumerable().Select(row => new Tabela
                {
                    ID = row.Field<long>("id"),
                    Nome = row.Field<string>("nome"),
                    PremiacaoDescrição = row.Field<string>("premiacao"),
                    Pontuacao = row.Field<long>("pontuacao"),
                    RegraDescricao = row.Field<string>("regraDescricao")

                }).ToList();

                
                return returnValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Tabela>();
            }
            finally
            {
                SQLConnection().Close();
            }
        }
        public static void SaveTabela(Tabela t)
        {
            try
            {
                SQLConnection().Open();
                if (t.ID == -1)
                {
                    using (var cmd = SQLConnection().CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Tabelas(Nome, premiacao, pontuacao, RegraDescricao )" +
                                          " values (@nome, @premiacao, @pontuacao, @regra)";

                        cmd.Parameters.AddWithValue("@Nome", t.Nome);
                        cmd.Parameters.AddWithValue("@premiacao", t.PremiacaoDescrição);
                        cmd.Parameters.AddWithValue("@pontuacao", t.Pontuacao);
                        cmd.Parameters.AddWithValue("@regra", t.RegraDescricao);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = SQLConnection().CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Tabelas SET nome=@nome, premiacao=@premiacao," +
                                          "pontuacao=@pontuacao, regra=@regra WHERE id=@id";
                        cmd.Parameters.AddWithValue("@id", t.ID);
                        cmd.Parameters.AddWithValue("@nome", t.Nome);
                        cmd.Parameters.AddWithValue("@premiacao", t.PremiacaoDescrição);
                        cmd.Parameters.AddWithValue("@pontuacao", t.Pontuacao);
                        cmd.Parameters.AddWithValue("@regra", t.RegraDescricao);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SQLConnection().Close();
            }
        }
        public static List<Time> GetTimes()
        {
            try
            {
                SQLConnection().Open();
                DataTable tb = new DataTable();
                using (var cmd = SQLConnection().CreateCommand())
                {
                    cmd.CommandText = "select * from times";
                    var da = new SQLiteDataAdapter(cmd.CommandText, SQLConnection());
                    da.Fill(tb);
                }

                List<Time> returnValue = tb.AsEnumerable().Select(row => new Time
                {
                    ID = row.Field<long>("id"),
                    Nome = row.Field<string>("nome"),
                    Jogador1 = row.Field<string>("jogador1"),
                    Jogador2 = row.Field<string>("jogador2"),
                    TabelaID = row.Field<long>("tabelaid")

                }).ToList();


                return returnValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Time>();
            }
            finally
            {
                SQLConnection().Close();
            }
        }

        public static void SaveTime(Time t)
        {
            try 
            {
                SQLConnection().Open();
                if (t.ID == -1)
                {
                    using (var cmd = SQLConnection().CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Times(nome, jogador1, jogador2,pontos, tabelaid )" +
                                          " values (@nome, @jogador1, @jogador2, @pontos, @tabelaid)";
                        cmd.Parameters.AddWithValue("@nome", t.Nome);
                        cmd.Parameters.AddWithValue("@jogador1", t.Jogador1);
                        cmd.Parameters.AddWithValue("@jogador2", t.Jogador2);
                        cmd.Parameters.AddWithValue("@pontos", t.pontos);
                        cmd.Parameters.AddWithValue("@tabelaid", t.TabelaID);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = SQLConnection().CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Times SET nome=@nome, jogador1=@jogador1," +
                                          "jogador2=@jogador2, pontos=@pontos, tabelaid=@tabelaid" +
                                          " WHERE id=@id";
                        cmd.Parameters.AddWithValue("@id", t.ID);
                        cmd.Parameters.AddWithValue("@nome", t.Nome);
                        cmd.Parameters.AddWithValue("@jogador1", t.Jogador1);
                        cmd.Parameters.AddWithValue("@jogador2", t.Jogador2);
                        cmd.Parameters.AddWithValue("@pontos", t.pontos);
                        cmd.Parameters.AddWithValue("@tabelaid", t.TabelaID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SQLConnection().Close();
            }
        }

        public static Tabela TabelaPorId(long id)
        {
            Tabela t = new Tabela();
            try
            {
                SQLConnection().Open();
                DataTable tb = new DataTable();
                using (var cmd = SQLConnection().CreateCommand())
                {
                    cmd.CommandText = "select * from tabelas where id="+id;
                    //cmd.Parameters.AddWithValue("@id", id);
                    var da = new SQLiteDataAdapter(cmd.CommandText, SQLConnection());
                    da.Fill(tb);
                }
                
                if (tb.Rows.Count > 0)
                {
                    var row = tb.Rows[0];
                    t = new Tabela()
                    {
                        ID = row.Field<long>("id"),
                        Nome = row.Field<string>("nome"),
                        PremiacaoDescrição = row.Field<string>("premiacao"),
                        Pontuacao = row.Field<long>("pontuacao"),
                        RegraDescricao = row.Field<string>("regraDescricao")
                    };
                }                   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SQLConnection().Close();
            }
            return t;
        }
        public static Time TimePorId(long id)
        {
            Time t = new Time();
            try
            {
                SQLConnection().Open();
                DataTable tb = new DataTable();
                using (var cmd = SQLConnection().CreateCommand())
                {
                    cmd.CommandText = "select * from times where id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var da = new SQLiteDataAdapter(cmd.CommandText, SQLConnection());
                    da.Fill(tb);
                }
                if (tb.Rows.Count > 0)
                {
                    var row = tb.Rows[0];
                    t = new Time()
                    {
                        ID = row.Field<long>("id"),
                        Nome = row.Field<string>("nome"),
                        Jogador1 = row.Field<string>("jogador1"),
                        Jogador2 = row.Field<string>("jogador2"),
                        TabelaID = row.Field<long>("tabelaid")
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SQLConnection().Close();
            }
            return t;
        }
    }
}
