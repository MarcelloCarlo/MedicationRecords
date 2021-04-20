using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Medication_Entity;

namespace Medication_DAL
{
    public class MedicationDAL
    {

        private static readonly string connStr = ConfigurationManager
            .ConnectionStrings["MedicationDB"].ConnectionString;

        public async Task<IEnumerable<MedicationEntity>> LoadAsync()
        {
            return await Task.FromResult(Load());
        }

        public IEnumerable<MedicationEntity> Load()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "sp_MedicationCRUD",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@StatementType", "Select");

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        MedicationEntity medicationEntity =
                        new MedicationEntity();
                        while (reader.Read())
                        {
                            yield return new MedicationEntity
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Patients = "" + reader["Patients"],
                                Drug = "" + reader["Drug"],
                                Dosage = "" + reader["Dosage"],
                                Date = "" + reader["Date"]

                            };
                        }
                    }
                }

            }
        }

        public async Task<MedicationEntity> ViewAsync(int id)
        {
            return await Task.FromResult(View(id));
        }

        public MedicationEntity View(int id)
        {
            MedicationEntity medicationEntity = new MedicationEntity();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "sp_MedicationCRUD",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@StatementType", "View");

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            medicationEntity.Id = Convert.ToInt32(reader["Id"]);
                            medicationEntity.Patients = "" + reader["Patients"];
                            medicationEntity.Drug = "" + reader["Drug"];
                            medicationEntity.Dosage = "" + reader["Dosage"];
                            medicationEntity.Date = "" + reader["Date"];
                        }
                    }
                }

            }

            return medicationEntity;
        }

        public async Task<MedicationEntity> InsertAsync(MedicationEntity medicationEntity)
        {
            return await Task.FromResult(Insert(medicationEntity));
        }

        public MedicationEntity Insert(MedicationEntity medicationEntity)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "sp_MedicationCRUD",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Patients",
                        medicationEntity.Patients);
                    cmd.Parameters.AddWithValue("@Drug", medicationEntity.Drug);
                    cmd.Parameters.AddWithValue("@Dosage", medicationEntity.Dosage);
                    cmd.Parameters.AddWithValue("@StatementType", "Insert");

                    conn.Open();

                    medicationEntity.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return medicationEntity;

        }

        public Task<bool> UpdateAsync(MedicationEntity medicationEntity)
        {
            bool result = true;
            return Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "sp_MedicationCRUD",
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        cmd.Parameters.AddWithValue("@Id", medicationEntity.Id);
                        cmd.Parameters.AddWithValue("@Patients",
                            medicationEntity.Patients);
                        cmd.Parameters.AddWithValue("@Drug", medicationEntity.Drug);
                        cmd.Parameters.AddWithValue("@Dosage", medicationEntity.Dosage);
                        cmd.Parameters.AddWithValue("@StatementType", "Update");

                        conn.Open();
                        int rowResult = cmd.ExecuteNonQuery();

                        if (rowResult == 0)
                        {
                            result = false;
                        }

                    }
                }

                return result;
            });
        }

        public Task<bool> DeleteAsync(MedicationEntity medicationEntity)
        {
            bool result = true;
            return Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "sp_MedicationCRUD",
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        cmd.Parameters.AddWithValue("@Id", medicationEntity.Id);
                        cmd.Parameters.AddWithValue("@StatementType", "Delete");

                        conn.Open();
                        int rowResult = cmd.ExecuteNonQuery();

                        if (rowResult == 0)
                        {
                            result = false;
                        }
                    }
                }

                return result;
            });
        }

        public async Task<IEnumerable<MedicationEntity>> CheckAsync(MedicationEntity medicationEntity)
        {
            return await Task.FromResult(Check(medicationEntity));
        }

        public IEnumerable<MedicationEntity> Check(MedicationEntity medicationEntity)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "sp_MedicationCRUD",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Patients",medicationEntity.Patients);
                    cmd.Parameters.AddWithValue("@Drug", medicationEntity.Drug);
                    cmd.Parameters.AddWithValue("@StatementType", "Check");

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new MedicationEntity
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                            };
                        }
                    }
                }
            }
        }
    }
}
