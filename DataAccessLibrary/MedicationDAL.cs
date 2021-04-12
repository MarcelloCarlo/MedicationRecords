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

        private static readonly string connStr = ConfigurationManager.ConnectionStrings["MedicationDB"].ConnectionString;

        public static List<T> LoadData<T>(string sql)
        {

            using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()))
            {

                return dbConnection.(sql).ToList();

            }
        }

        public static List<T> SearchData<T>(string sql, T data)
        {

            using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()))
            {

                return dbConnection.Query<T>(sql, data).ToList();
            }
        }

        public Task<MedicationEntity> InsertAsync(MedicationEntity medicationEntity)
        {
            return (Task<MedicationEntity>)Task.Run(() =>
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
                        cmd.Parameters.AddWithValue("@Patients", medicationEntity.Patients);
                        cmd.Parameters.AddWithValue("@Drug", medicationEntity.Drug);
                        cmd.Parameters.AddWithValue("@Dosage", medicationEntity.Dosage);
                        cmd.Parameters.AddWithValue("@Date", medicationEntity.Date);
                        cmd.Parameters.AddWithValue("@StatementType", "Insert");

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            });
        }
    }
}
