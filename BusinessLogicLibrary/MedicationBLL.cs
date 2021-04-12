using static DataAccessLibrary.MedicationDAL;
using Medication_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication_BLL
{
  public static class MedicationBLL
  {
    public static int CreateRecord(string txtPatients,
      string txtDrug,
      decimal txtDosage,
      DateTime Date)

    {

      Medication data = new Medication
      {

        Patients = txtPatients,
        Drug = txtDrug,
        Dosage = txtDosage,
        Date = Date

      };

      string sqlScript = @"INSERT INTO [dbo].[Medication] ([Patients]
           ,[Drug]
           ,[Dosage]
           ,[Date])
        VALUES
           (@Patients,@Drug,@Dosage,@Date);";

      return SaveData(sqlScript, data);

    }

    public static int UpdateRecord(int Id, string txtPatients,
      string txtDrug,
      decimal txtDosage,
      DateTime Date)
    {

      Medication data = new Medication
      {

        Id = Id,
        Patients = txtPatients,
        Drug = txtDrug,
        Dosage = txtDosage,
        Date = Date

      };

      string sqlScript = @"UPDATE [dbo].[Medication] SET 
           [Patients] = @Patients,
           [Drug] = @Drug,
           [Dosage] = @Dosage,
           [Date] = @Date
        WHERE [Id] = @Id";

      return SaveData(sqlScript, data);

    }

    public static int DeleteRecord(int Id)
    {

      Medication data = new Medication
      {

        Id = Id

      };

      string sqlScript = @"DELETE FROM [dbo].[Medication]
              WHERE Id = @Id";

      return SaveData(sqlScript, data);

    }

    public static List<Medication> LoadRecords()
    {

      string sqlScript = @"SELECT [Id] ,[Patients] ,[Drug] ,[Dosage] ,[Date]
                          FROM [dbo].[Medication]";

      return LoadData<Medication>(sqlScript);

    }

    public static List<Medication> SearchQueryById(int id)
    {

      Medication data = new Medication
      {

        Id = id

      };

      string sqlScript = @"SELECT * FROM [dbo].[Medication] WHERE Id = @Id";

      return SearchData(sqlScript, data);

    }
  }
}
