using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using kol01.DTOs.Requests;
using kol01.DTOs.Responses;

namespace kol01.DAL
{
    public class SqlServerDbService : IDbService
    {
        private readonly string _connectionString = "Data Source=db-mssql.pjwstk.edu.pl; " 
                                                    + "Initial Catalog=s19991; " 
                                                    + "User Id=apbds19991;"
                                                    + " Password=admin";
        
        public GetPrescriptionsResponse GetPrescriptions(int id)
        {
            GetPrescriptionsResponse response;
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "select * from Prescription " 
                                      + "where Prescription.IdPrescription = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new Exception($"No prescriptions found for IdPrescription: {id}");
                    }
                    response = new GetPrescriptionsResponse
                    {
                        IdPrescription = int.Parse(reader["IdPrescription"].ToString()),
                        Date = DateTime.Parse(reader["Date"].ToString())
                    };
                }

                command.CommandText = "select * from Medicament "
                                      + "inner join Prescription_Medicament on "
                                      + "Prescription_Medicament.IdMedicament = Medicament.IdMedicament "
                                      + "where Prescription_Medicament.IdPrescription = @Id;";
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        throw new Exception($"No medicaments found for IdPrescription: {id}");
                    }
                    response.Medicaments = new List<MedicamentsResponse>();
                    while (reader.Read())
                    {
                        var medicament = new MedicamentsResponse
                        {
                            IdMedicament = int.Parse(reader["IdMedicament"].ToString()),
                            Dose = int.Parse(reader["Dose"].ToString()),
                            Details = reader["Details"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Type = reader["Type"].ToString()
                        };
                        response.Medicaments.Add(medicament);
                    }
                }
            }
            return response;
        }

        public PostPrescriptionResponse PostPrescription(PostPrescriptionRequest request)
        {
            PostPrescriptionResponse response;

            if (DateTime.Compare(request.DueDate, request.Date) <= 0)
            {
                throw new Exception("Due date is smaller than date");
            }
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "select * from Doctor where IdDoctor = @IdDoctor;";
                command.Parameters.AddWithValue("IdDoctor", request.IdDoctor);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        transaction.Rollback();
                        throw new Exception($"No doctors with IdDoctor: {request.IdDoctor}");
                    }
                }
                
                command.CommandText = "select * from Patient where IdPatient = @IdPatient;";
                command.Parameters.AddWithValue("IdPatient", request.IdPatient);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        transaction.Rollback();
                        throw new Exception($"No patients with IdPatient: {request.IdPatient}");
                    }
                }

                int idPrescription;
                command.CommandText = "select count(*)+1 as newId from Prescription ;";
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        transaction.Rollback();
                        throw new Exception($"Failed counting rows!");
                    }

                    idPrescription = int.Parse(reader["newId"].ToString());
                }

                command.CommandText = "insert into Prescription(IdPrescription, Date, DueDate, IdPatient, IdDoctor) "
                                      + "values (@IdPrescription, @Date, @DueDate, @IdPatient, @IdDoctor);";
                command.Parameters.AddWithValue("IdPrescription", idPrescription);
                command.Parameters.AddWithValue("Date", request.Date);
                command.Parameters.AddWithValue("DueDate", request.DueDate);
                command.Parameters.AddWithValue("IdPatient", request.IdPatient);
                command.Parameters.AddWithValue("IdDoctor", request.IdDoctor);
                command.Parameters.Clear();
                using (var reader = command.ExecuteReader())
                {
                    response = new PostPrescriptionResponse
                    {
                        IdPrescription = idPrescription,
                        Date = request.Date,
                        DueDate = request.DueDate,
                        IdDoctor = request.IdDoctor,
                        IdPatient = request.IdPatient
                    };
                }
            }

            return response;
        }
    }
}