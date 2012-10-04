
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/04/2012 22:45:24
-- Generated from EDMX file: C:\Users\Morten\Documents\Visual Studio 2012\Projects\AS40\SchedulingBenchmarking\BenchDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BenchDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BenchDatabases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BenchDatabases];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BenchDatabases'
CREATE TABLE [dbo].[BenchDatabases] (
    [logId] int IDENTITY(1,1) NOT NULL,
    [state] nvarchar(max)  NOT NULL,
    [dateAdded] datetime  NOT NULL,
    [jobId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [logId], [jobId] in table 'BenchDatabases'
ALTER TABLE [dbo].[BenchDatabases]
ADD CONSTRAINT [PK_BenchDatabases]
    PRIMARY KEY CLUSTERED ([logId], [jobId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------