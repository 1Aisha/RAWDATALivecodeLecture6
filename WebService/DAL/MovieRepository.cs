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
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();
                var sql = string.Format(
                    "select id, title, production_year from title limit {0} offset {1}",
                    limit, offset);
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
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();
                var sql = string.Format(
                    "select id, title, production_year from title where id =  {0}",
                    id);
                var cmd = new MySqlCommand(sql, connection);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows && rdr.Read())
                    {
                        return new Movie
                        {
                            Id = rdr.GetInt32(0),
                            Title = rdr.GetString(1),
                            Year = rdr.GetInt32(2)
                        };
                    }
                }
            }
            return null;
        }
    }
}