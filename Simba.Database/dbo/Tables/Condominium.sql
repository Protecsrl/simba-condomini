﻿CREATE TABLE [dbo].[Condominium] (
    [Oid]            INT            IDENTITY (1, 1) NOT NULL,
    [Comune]         INT            NOT NULL,
    [NomeCondominio] NVARCHAR (50)  NOT NULL,
    [Indirizzo]      NVARCHAR (500) NOT NULL,
    [PartitaIva]     NVARCHAR (11)  NOT NULL,
    [Latitudine]     FLOAT (53)     NULL,
    [Longitudine]    FLOAT (53)     NULL,
    [Code] NVARCHAR(6) NULL, 
    CONSTRAINT [PK_Condominium] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Condominuim_Comune] FOREIGN KEY ([Comune]) REFERENCES [dbo].[Comuni] ([Oid])
);

