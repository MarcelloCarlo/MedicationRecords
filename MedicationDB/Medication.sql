CREATE TABLE [dbo].[Medication]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Patients] VARCHAR(50) NOT NULL, 
    [Drug] VARCHAR(50) NOT NULL, 
    [Dosage] DECIMAL(7, 4) NOT NULL, 
    [Date] DATETIME NOT NULL 
)
