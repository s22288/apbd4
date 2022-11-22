using apbd4.model;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace apbd4.Service
{
    public class IDBServiceImplementation : IDBinterface

    {
        private string conString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";



        public async Task<Animal> addAnimalToDb(Animal animal)
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();
                var sql = "INSERT INTO Animal(Name,Description,Category,Area) VALUES(@Name,@Description,@Category,@Area)";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                    cmd.Parameters.AddWithValue("@Name", animal.name);
                    cmd.Parameters.AddWithValue("@Description", animal.Description);
                    cmd.Parameters.AddWithValue("@Category", animal.Category);
                    cmd.Parameters.AddWithValue("@Area", animal.Area);




                    cmd.ExecuteNonQuery();
                }
            }

            return animal;
        }

        public async Task<Boolean> checkIfRecordExists(string id)
        {

            SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [Table] WHERE IdAnimal = @IdAnimal", new SqlConnection(conString));
            check_User_Name.Parameters.AddWithValue("@user", id);
            int UserExist = (int)check_User_Name.ExecuteScalar();

            if (UserExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> deleteAnimalById(String id)
        {

            try
            {
                using (var sc = new SqlConnection(conString))
                using (var cmd = sc.CreateCommand())
                {
                    sc.Open();
                    cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                    cmd.Parameters.AddWithValue("@IdAnimal", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Animal> getAllAnimals(String order)
        {
           List<Animal> animals = new List<Animal>();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "Select * From Animal order by " + order;
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    animals.Add(new Animal
                    {
                        IdAnimal = int.Parse(dr["IdAnimal"].ToString()),
                        name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                     
                    }); 

                }
            }

           
            return animals; 
        }

        public async Task<String> updateAnimal(Animal animal, string id)
        {


            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area Where IdAnimal = @IdAnimal";
                
                command.Parameters.AddWithValue("@Name", animal.name);
                command.Parameters.AddWithValue("@Description", animal.Description);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);
                command.Parameters.AddWithValue("@IdAnimal", id);



                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }

            return id;
        }
    }
}
