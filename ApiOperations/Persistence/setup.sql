CREATE DATABASE [Andreani_DB]
GO

USE [Andreani_DB]
GO
CREATE TABLE [dbo].[Operation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](MAX) NOT NULL,
	[FirstArgument] [INT] NOT NULL,
	[SecondArgument] [INT] NOT NULL,
    [Total] [INT] NULL
	PRIMARY KEY (ID)
)

GO