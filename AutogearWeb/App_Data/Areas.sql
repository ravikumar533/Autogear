/*
   08 January 201608:31:04 PM
   User: 
   Server: CONEVO
   Database: AutogearDB
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
CREATE TABLE dbo.Areas
	(
	AreaId int NOT NULL IDENTITY (1, 1),
	Name nvarchar(150) NOT NULL,
	Description nvarchar(500) NOT NULL,
	CreatedDate datetime NOT NULL,
	CreatedBy nvarchar(128) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Areas ADD CONSTRAINT
	PK_Areas PRIMARY KEY CLUSTERED 
	(
	AreaId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Areas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
