USE [TestDb]
GO

/****** Object: Table [dbo].[student] Script Date: 09-07-2022 00:01:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[student] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [StudentName] NVARCHAR (50) NULL,
    [Age]         INT           NULL
);


