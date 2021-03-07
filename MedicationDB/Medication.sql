CREATE TABLE [dbo].[Medication]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Patients] VARCHAR(50) NOT NULL, 
    [Drug] VARCHAR(50) NOT NULL, 
    [Dosage] DECIMAL(7, 4) NOT NULL, 
    [Date] DATETIME NOT NULL 
)
GO;

CREATE PROCEDURE masterStoredProcedure (@Id            INTEGER = NULL,  
                                          @Patients    VARCHAR(50) = NULL,  
                                          @Drug     VARCHAR(50) = NULL,  
                                          @Dosage        DECIMAL(7, 4) = NULL,  
                                          @Date          DATETIME = NULL,  
                                          @StatementType NVARCHAR(20) = '')  
AS  
  BEGIN  
      IF @StatementType = 'Insert'  
        BEGIN  
            INSERT INTO [dbo].[Medication]  
                        ([Patients],  
                         [Drug],  
                         [Dosage],  
                         [Date])  
            VALUES     ( @Patients,  
                         @Drug,  
                         @Dosage,  
                         @Date)  
        END  
  
      IF @StatementType = 'Select'  
        BEGIN  
            SELECT *  
            FROM   [dbo].[Medication]  
        END  
  
      IF @StatementType = 'Update'  
        BEGIN  
            UPDATE [dbo].[Medication]  
            SET    [Patients] = @Patients,  
                   [Drug] = @Drug,  
                   [Dosage] = @Dosage,  
                   [Date] = @Date
            WHERE  [Id] = @Id 
        END  
      ELSE IF @StatementType = 'Delete'  
        BEGIN  
            DELETE FROM [dbo].[Medication]  
            WHERE  [Id] = @Id  
        END  
  END    
