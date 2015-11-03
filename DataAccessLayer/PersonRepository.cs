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

        public int GetNewId()
        {
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();

                var cmd = new MySqlCommand("select max(id) from name", connection);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows && rdr.Read())
                    {
                        return rdr.GetInt32(0) + 1;
                    }
                }
            }
            return 1;
        }

        public void Add(Person person)
        {
            person.Id = GetNewId();
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();

                var cmd = new MySqlCommand(
                    "insert into name(id,name,gender) values(@id, @name, @gender)", connection);
                cmd.Parameters.AddWithValue("@id", person.Id);
                cmd.Parameters.AddWithValue("@name", person.Name);
                cmd.Parameters.AddWithValue("@gender", person.Gender);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Person person)
        {
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();

                var cmd = new MySqlCommand(
                    "update name set name = @name, gender = @gender where id = @id", connection);
                cmd.Parameters.AddWithValue("@id", person.Id);
                cmd.Parameters.AddWithValue("@name", person.Name);
                cmd.Parameters.AddWithValue("@gender", person.Gender);
                cmd.ExecuteNonQuery();
            }
        }
    }
}