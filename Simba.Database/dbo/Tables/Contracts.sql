CREATE TABLE [dbo].[Contracts] (
    [Oid]         INT            NOT NULL,
    [Testo]       NVARCHAR (MAX) NULL,
    [DataInizio]  DATETIME2 (7)  NULL,
    [DataFine]    DATETIME2 (7)  NULL,
    [Condominium] INT            NULL,
    CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Contracts_Condominium] FOREIGN KEY ([Condominium]) REFERENCES [dbo].[Condominium] ([Oid])
);

