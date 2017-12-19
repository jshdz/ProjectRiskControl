
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/10/2017 21:16:08
-- Generated from EDMX file: E:\Documents\Visual Studio 2017\Projects\NET\ProjectRiskControl\PRC.Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PRC_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Impactos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Impactos];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Impactos'
CREATE TABLE [dbo].[Impactos] (
    [idImpacto] int IDENTITY(1,1) NOT NULL,
    [categoria] nvarchar(100)  NOT NULL,
    [descripcion] nvarchar(500)  NOT NULL,
    [valor] int  NOT NULL
);
GO

-- Creating table 'Probabilidades'
CREATE TABLE [dbo].[Probabilidades] (
    [idProbabilidad] int IDENTITY(1,1) NOT NULL,
    [categoria] nvarchar(50)  NOT NULL,
    [descripcion] nvarchar(500)  NOT NULL,
    [rangoInicio] int  NOT NULL,
    [rangoFin] int  NOT NULL,
    [valor] int  NOT NULL
);
GO

-- Creating table 'Riesgos'
CREATE TABLE [dbo].[Riesgos] (
    [idRiesgo] int IDENTITY(1,1) NOT NULL,
    [puntuajePI] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(500)  NOT NULL,
    [categoria] nvarchar(100)  NOT NULL,
    [disparador] nvarchar(500)  NULL,
    [accionarDisparador] nvarchar(500)  NULL,
    [tipoRespuesta] nvarchar(100)  NULL,
    [descripcionRespuesta] nvarchar(500)  NULL,
    [resultadoRespuesta] nvarchar(500)  NULL,
    [resultadoEsperado] nvarchar(500)  NULL,
    [encargadoMonitoreo] nvarchar(150)  NULL,
    [encargadoRespuesta] nvarchar(150)  NULL,
    [nivel] nvarchar(max)  NOT NULL,
    [color] nvarchar(max)  NOT NULL,
    [idImpacto] int  NOT NULL,
    [idProbabilidad] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idImpacto] in table 'Impactos'
ALTER TABLE [dbo].[Impactos]
ADD CONSTRAINT [PK_Impactos]
    PRIMARY KEY CLUSTERED ([idImpacto] ASC);
GO

-- Creating primary key on [idProbabilidad] in table 'Probabilidades'
ALTER TABLE [dbo].[Probabilidades]
ADD CONSTRAINT [PK_Probabilidades]
    PRIMARY KEY CLUSTERED ([idProbabilidad] ASC);
GO

-- Creating primary key on [idRiesgo] in table 'Riesgos'
ALTER TABLE [dbo].[Riesgos]
ADD CONSTRAINT [PK_Riesgos]
    PRIMARY KEY CLUSTERED ([idRiesgo] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idImpacto] in table 'Riesgos'
ALTER TABLE [dbo].[Riesgos]
ADD CONSTRAINT [FK_RiesgoImpacto]
    FOREIGN KEY ([idImpacto])
    REFERENCES [dbo].[Impactos]
        ([idImpacto])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RiesgoImpacto'
CREATE INDEX [IX_FK_RiesgoImpacto]
ON [dbo].[Riesgos]
    ([idImpacto]);
GO

-- Creating foreign key on [idProbabilidad] in table 'Riesgos'
ALTER TABLE [dbo].[Riesgos]
ADD CONSTRAINT [FK_RiesgoProbabilidad]
    FOREIGN KEY ([idProbabilidad])
    REFERENCES [dbo].[Probabilidades]
        ([idProbabilidad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RiesgoProbabilidad'
CREATE INDEX [IX_FK_RiesgoProbabilidad]
ON [dbo].[Riesgos]
    ([idProbabilidad]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------