using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// The movie repsitory.
    /// 
    /// Defines the inderface to movies in the project
    /// </summary>
    public class MovieRepository
    {
        /// <summary>
        /// Get all movies from given a limit and offset.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IEnumerable<Movie> GetAll(int limit = 10, int offset = 0)
        {
            // create the SQL statement
            var sql = string.Format(
                    "select id, title, production_year from title limit {0} offset {1}",
                    limit, offset);
            // fetch the selected movies
            foreach (var movie in ExecuteQuery(sql))
                yield return movie;
        }

        /// <summary>
        /// Execute a query that return movie domain objects given
        /// a SQL statement.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static IEnumerable<Movie> ExecuteQuery(string sql)
        {
            // create the connection
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                // open the connection to the database
                connection.Open();
                // create the command
                var cmd = new MySqlCommand(sql, connection);
                // get the reader (cursor)
                using (var rdr = cmd.ExecuteReader())
                {
                    // as long as we have rows we can read
                    while (rdr.HasRows && rdr.Read())
                    {
                        // return a movie object and yield
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

        /// <summary>
        /// Get one specific movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Movie GetById(int id)
        {
            // create the SQL statement
            var sql = string.Format(
                    "select id, title, production_year from title where id =  {0}",
                    id);
            // We know that we either will get zero or one objects back, i.e. 
            // id is the primary key. FirdstOrDefault return the object or null
            return ExecuteQuery(sql).FirstOrDefault();
        }
    }
}