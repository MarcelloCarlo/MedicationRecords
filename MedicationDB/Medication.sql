USE [master]
GO
/****** Object:  Database [MedicationDB]    Script Date: 24/04/2021 01:52:34 ******/
DROP DATABASE IF EXISTS [MedicationDB]
GO
CREATE DATABASE [MedicationDB]
GO

USE [MedicationDB]
GO
/****** Object:  Table [dbo].[Medication]    Script Date: 24/04/2021 01:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Patients] [varchar](50) NOT NULL,
	[Drug] [varchar](50) NOT NULL,
	[Dosage] [decimal](10, 3) NOT NULL,
	[Date] [varchar](12) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_MedicationCRUD]    Script Date: 24/04/2021 01:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_MedicationCRUD]
(
	@Id INTEGER = NULL
	,@Patients VARCHAR(50) = NULL
	,@Drug VARCHAR(50) = NULL
	,@Dosage VARCHAR(10) = NULL
    ,@Date VARCHAR(12) = NULL
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

    IF @StatementType = 'View'
	BEGIN
		SELECT * FROM [dbo].[Medication]
            WHERE [Id] = @Id
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

    IF @StatementType = 'Check' 
    BEGIN 
        SELECT [Id] FROM [dbo].[Medication]
            WHERE [Patients] = @Patients AND [Drug] = @Drug AND [Date] = FORMAT (GETDATE(),'MM/dd/yyyy')
    END
END
GO
USE [master]
GO
ALTER DATABASE [MedicationDB] SET  READ_WRITE 
GO
