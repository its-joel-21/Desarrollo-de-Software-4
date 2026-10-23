using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrudWindowsForms.Models;

namespace CrudWindowsForms.Data
{
    public class PeopleDB
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["CrudWindowsForms"].ConnectionString;

        public List<People> Get()
        {
            var people = new List<People>();
            const string query = "SELECT Id, Name, Age FROM dbo.People ORDER BY Id;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) people.Add(new People { Id = reader.GetInt32(0), Name = reader.GetString(1), Age = reader.GetInt32(2) });
            }
            return people;
        }

        public List<People> Search(string name)
        {
            var people = new List<People>();
            const string query = "SELECT Id, Name, Age FROM dbo.People WHERE Name LIKE @Name ORDER BY Id;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = "%" + name + "%";
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) people.Add(new People { Id = reader.GetInt32(0), Name = reader.GetString(1), Age = reader.GetInt32(2) });
            }
            return people;
        }

        public People GetById(int id)
        {
            const string query = "SELECT Id, Name, Age FROM dbo.People WHERE Id = @Id;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new People { Id = reader.GetInt32(0), Name = reader.GetString(1), Age = reader.GetInt32(2) };
                }
            }
        }

        public int Add(string name, int age)
        {
            const string query = "INSERT INTO dbo.People (Name, Age) VALUES (@Name, @Age);";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = name;
                command.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public int Update(int id, string name, int age)
        {
            const string query = "UPDATE dbo.People SET Name = @Name, Age = @Age WHERE Id = @Id;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = name;
                command.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            const string query = "DELETE FROM dbo.People WHERE Id = @Id;";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
