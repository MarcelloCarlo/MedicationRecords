USE [master]
GO

DROP DATABASE IF EXISTS [MedicationDB]
GO

CREATE DATABASE [MedicationDB]
GO

USE [MedicationDB]
GO

DROP TABLE IF EXISTS [dbo].[Medication]
CREATE TABLE [dbo].[Medication]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY
	,[Patients] VARCHAR(50) NOT NULL
	,[Drug] VARCHAR(50) NOT NULL
	,[Dosage] DECIMAL(7, 4) NOT NULL
	,[Date] VARCHAR(12) NOT NULL
)
GO

CREATE PROCEDURE sp_MedicationCRUD
(
	@Id INTEGER = NULL
	,@Patients VARCHAR(50) = NULL
	,@Drug VARCHAR(50) = NULL
	,@Dosage VARCHAR(10) = NULL
	,@StatementType NVARCHAR(20) = NULL
)
AS
BEGIN
	IF @StatementType = 'Insert'
	BEGIN
	SET NOCOUNT ON;

	--DECLARED VARIABLES
	DECLARE @tblPrimId TABLE(Id INT)

	INSERT INTO [dbo].[Medication]
	(
		[Patients]
		,[Drug]
		,[Dosage]
		,[Date]
	)
	OUTPUT inserted.Id INTO @tblPrimId
	VALUES
	( 
		@Patients
		,@Drug
		,@Dosage
		,FORMAT (GETDATE(),'MM/dd/yyyy')
	)

    SELECT [Id] FROM [Medication] WHERE [Id] = (select ID from @tblPrimId) 
	END

	IF @StatementType = 'Select'
	BEGIN
		SELECT * FROM [dbo].[Medication]
	END

	IF @StatementType = 'Update'
	BEGIN
		UPDATE [dbo].[Medication]
			SET [Patients] = @Patients
				,[Drug] = @Drug
				,[Dosage] = @Dosage
				,[Date] = FORMAT (GETDATE(),'MM/dd/yyyy')
		WHERE [Id] = @Id
	END

	ELSE IF @StatementType = 'Delete'
	BEGIN
		DELETE FROM [dbo].[Medication]
		WHERE [Id] = @Id
	END
END
