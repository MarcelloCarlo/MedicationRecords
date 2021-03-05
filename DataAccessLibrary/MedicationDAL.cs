using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
  public static class MedicationDAL
  {
    public static string GetConnectionString(string connectioName = "MedicationDB")
    {

      return ConfigurationManager.ConnectionStrings[connectioName].ConnectionString;

    }

    public static List<T> LoadData<T>(string sql)
    {

      using ( IDbConnection dbConnection = new SqlConnection(GetConnectionString()) )
      {

        return dbConnection.Query<T>(sql).ToList();

      }
    }

    public static List<T> SearchData<T>(string sql, T data)
    {

      using (IDbConnection dbConnection = new SqlConnection(GetConnectionString()) )
      {

        return dbConnection.Query<T>(sql, data).ToList();
      }
    }

    public static int SaveData<T>(string sql, T data)
    {

      using ( IDbConnection dbConnection = new SqlConnection(GetConnectionString()) )
      {

        return dbConnection.Execute(sql, data);

      }
    }
  }
}
