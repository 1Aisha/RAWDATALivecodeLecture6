using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class PersonRepository
    {
        public IEnumerable<Person> GetAll(int limit = 10, int offset = 0)
        {
            var sql = string.Format(
                    "select id, name, gender from name limit {0} offset {1}",
                    limit, offset);
            foreach (var person in ExecuteQuery(sql))
                yield return person;
        }

        private static IEnumerable<Person> ExecuteQuery(string sql)
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
                        yield return new Person
                        {
                            Id = rdr.GetInt32(0),
                            Name = rdr.GetString(1),
                            Gender = rdr.GetString(2)
                        };
                    }
                }
            }
        }

        public Person GetById(int id)
        {
            var sql = string.Format(
                    "select id, name, gender from name where id =  {0}", id);
            return ExecuteQuery(sql).FirstOrDefault();
        }
    }
}