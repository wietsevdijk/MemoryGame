    IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'MemoryDatabase')
  BEGIN
    CREATE DATABASE [MemoryDatabase]


    END

USE [MemoryDatabase]
GO

/****** Object: Table [dbo].[Games] Script Date: 22/01/2024 18:53:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Games] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [PlayerName]    VARCHAR (255) NOT NULL,
    [CardCount]     INT           NOT NULL,
    [Score]         FLOAT (53)    NOT NULL,
    [TimeElapsed]   FLOAT (53)    NOT NULL,
    [AttemptsTaken] INT           NOT NULL
);


