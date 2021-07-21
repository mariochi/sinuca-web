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

        public static SQLiteConnection SQLConnection()
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
                                      " tabelaid integer, "+
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
                        //cmd.Parameters.AddWithValue("@id", t.ID);
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
                        cmd.CommandText = "UPDATE Clientes SET Nome=@Nome, premiacao=@premiacao," +
                                          "pontuacao=@pontuacao, regra=@regra WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("@id", t.ID);
                        cmd.Parameters.AddWithValue("@Nome", t.Nome);
                        cmd.Parameters.AddWithValue("@premiacao", t.PremiacaoDescrição);
                        cmd.Parameters.AddWithValue("@pontuacao", t.Pontuacao);
                        cmd.Parameters.AddWithValue("@regra", t.RegraDescricao);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLConnection().Close();
            }
        }
        public static List<Time> GetTimes()
        {
            SQLConnection().Open();
            SQLConnection().Close();
            return null;
        }
    }
}
