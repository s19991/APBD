using System;
using System.Data.SqlClient;
using c08.DTOs.Responses;

namespace c08.DAL
{
    public class SqlServerDbService : IDbService
    {
        private readonly string _connectionString = "Data Source=db-mssql.pjwstk.edu.pl; " 
                                                    + "Initial Catalog=s19991; " 
                                                    + "User Id=apbds19991;"
                                                    + " Password=admin";


        public GetAnimalsResponse GetAnimals(string orderBy)
        {
            GetAnimalsResponse response;
            if (orderBy == null)
            {
                orderBy = "DateOfAdmission";
            }
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
            
                command.CommandText = "select a.Name, a.AnimalType, a.DateOfAdmission, o.LastNameOfOwner " 
                                      + "from Animal a inner join Owner o on o.IdOwner = a.IdOwner " 
                                      + "order by @orderBy";
                command.Parameters.AddWithValue("orderBy", orderBy);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new Exception($"Some error occured");
                    }
                    response = new GetAnimalsResponse
                    {
                        Name = reader["Name"].ToString(),
                        AnimalType = reader["AnimalType"].ToString(),
                        DateOfAdmission = DateTime.Parse(reader["DateOfAdmission"].ToString()),
                        LastNameOfOwner = reader["LastNameOfOwner"].ToString()
                    };
                }
            }

            return response;
        }
    }
}