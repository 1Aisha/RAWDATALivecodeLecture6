using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using WebService.Models;

namespace WebService.DAL
{
    public class MovieRepository
    {
        public IEnumerable<Movie> GetAll(int limit = 10, int offset = 0)
        {
            var sql = string.Format(
                    "select id, title, production_year from title limit {0} offset {1}",
                    limit, offset);
            foreach (var movie in ExecuteQuery(sql))
                yield return movie;
        }

        private static IEnumerable<Movie> ExecuteQuery(string sql)
        {
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();
                
                var cmd = new MySqlCommand(sql, connection);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.HasRows && rdr.Read())
                    {
                        yield return new Movie
                        {
                            Id = rdr.GetInt32(0),
                            Title = rdr.GetString(1),
                            Year = rdr.GetInt32(2)
                        };
                    }
                }
            }
        }

        public Movie GetById(int id)
        {
            var sql = string.Format(
                    "select id, title, production_year from title where id =  {0}",
                    id);
            return ExecuteQuery(sql).FirstOrDefault();
        }
    }
}