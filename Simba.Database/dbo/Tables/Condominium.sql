CREATE TABLE [dbo].[Condominium] (
    [Oid]            INT            IDENTITY (1, 1) NOT NULL,
    [Comune]         INT            NOT NULL,
    [NomeCondominio] NVARCHAR (50)  NOT NULL,
    [Indirizzo]      NVARCHAR (500) NOT NULL,
    [PartitaIva]     NVARCHAR (11)  NOT NULL,
    [Latitudine]     FLOAT (53)     NULL,
    [Longitudine]    FLOAT (53)     NULL,
    CONSTRAINT [PK_Condominium] PRIMARY KEY CLUSTERED ([Oid] ASC)
);

