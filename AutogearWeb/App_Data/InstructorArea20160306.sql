/*
   Sunday, March 6, 201610:42:25 AM
   User: 
   Server: mohan
   Database: AutogearDb
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.InstructorArea
	(
	Id int NOT NULL IDENTITY (1, 1),
	InstructorID int NOT NULL,
	AreaId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.InstructorArea ADD CONSTRAINT
	PK_InstructorArea PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.InstructorArea SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
