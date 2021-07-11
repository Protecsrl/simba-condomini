CREATE TABLE [dbo].[Provincia]
(
	[Oid] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Descrizione] [varchar](1000) NULL,
	[Sigla] [varchar](2) NULL,
	[CodiceProvincia] [varchar](3) NULL,
	[Regione] [int] NULL,
	CONSTRAINT [FK_Provincia_Regione] FOREIGN KEY ([Regione]) REFERENCES [dbo].[Regione] ([Oid]), 
    CONSTRAINT [PK_Provincia] PRIMARY KEY ([Oid])
)
