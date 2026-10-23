using CrudWindowsForms.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CrudWindowsForms;

namespace CrudWindowsForms.Data
{
    public class CRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["CrudWindowsForms"].ConnectionString;

        public List<People> Get()
        {
            using (var db = new PeopleDataContextDataContext(connectionString))
            {
                return db.Peoples.OrderBy(p => p.Id).ToList();
            }
        }

        public List<People> Search(string name)
        {
            using (var db = new PeopleDataContextDataContext(connectionString))
            {
                return db.Peoples
                    .Where(p => p.Name.Contains(name))
                    .OrderBy(p => p.Id)
                    .ToList();
             }
        }

        public People GetById(int id)
        {
            using (var db = new PeopleDataContextDataContext(connectionString))
            {
                return db.Peoples.FirstOrDefault(p => p.Id == id);
            }
        }

        public int Add(string name, int age)
        {
            using (var db = new PeopleDataContextDataContext(connectionString))
            {
                var nuevaPersona = new People { Name = name, Age = age };

                db.Peoples.InsertOnSubmit(nuevaPersona);
                db.SubmitChanges(); // Aquí se ejecuta el INSERT real en la BD

                return 1;
            }
        }

        public int Update(int id, string name, int age)
        {
            using (var db = new PeopleDataContextDataContext(connectionString))
            {
                var persona = db.Peoples.FirstOrDefault(p => p.Id == id);

                if (persona != null)
                {
                    persona.Name = name;
                    persona.Age = age;
                    db.SubmitChanges();
                    return 1;
                }
                return 0;
            }
        }

        public int Delete(int id)
        {
            using (var db = new PeopleDataContextDataContext(connectionString))
            {
                var persona = db.Peoples.FirstOrDefault(p => p.Id == id);

                if (persona != null)
                {
                    db.Peoples.DeleteOnSubmit(persona);
                    db.SubmitChanges();
                    return 1;
                }
                return 0;
            }
        }
    }
}