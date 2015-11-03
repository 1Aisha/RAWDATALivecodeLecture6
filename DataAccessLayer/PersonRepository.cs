using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// The person repsitory.
    /// 
    /// Defines the inderface to persons in the project
    /// </summary>
    public class PersonRepository
    {
        /// <summary>
        /// Get persons from given a limit and offset.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IEnumerable<Person> GetAll(int limit = 10, int offset = 0)
        {
            // create the SQL statement
            var sql = string.Format(
                    "select id, name, gender from name limit {0} offset {1}",
                    limit, offset);
            // fetch the person objects
            foreach (var person in ExecuteQuery(sql))
                yield return person;
        }

        /// <summary>
        /// Execute a query that return person domain objects given
        /// a SQL statement.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static IEnumerable<Person> ExecuteQuery(string sql)
        {
            // create the connection
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                // open the connection
                connection.Open();
                // create the command
                var cmd = new MySqlCommand(sql, connection);
                // get a reader
                using (var rdr = cmd.ExecuteReader())
                {
                    // as long as we have rows we can read
                    while (rdr.HasRows && rdr.Read())
                    {
                        // return a person object and yield
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

        /// <summary>
        /// Get one specific person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetById(int id)
        {
            // create a SQL statement
            var sql = string.Format(
                    "select id, name, gender from name where id =  {0}", id);
            // We know that we either will get zero or one objects back, i.e. 
            // id is the primary key. FirdstOrDefault return the object or null
            return ExecuteQuery(sql).FirstOrDefault();
        }

        /// <summary>
        /// Get the next id from the db
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
            // create the connnection
            using (var connection = new MySqlConnection(
                "server=localhost;database=imdb;uid=bulskov;pwd=henrik"))
            {
                connection.Open();

                var cmd = new MySqlCommand("select max(id) from name", connection);
                using (var rdr = cmd.ExecuteReader())
                {
                    // if we get something, return that plus one
                    if (rdr.HasRows && rdr.Read())
                    {
                        return rdr.GetInt32(0) + 1;
                    }
                }
            }
            // assume that this is the first row
            return 1;
        }

        /// <summary>
        /// Add a new person to the database
        /// </summary>
        /// <param name="person"></param>
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

        /// <summary>
        /// Update a person in the database
        /// </summary>
        /// <param name="person"></param>
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