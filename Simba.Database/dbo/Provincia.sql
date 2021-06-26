CREATE TABLE [dbo].[Provincia]
(
	[OID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[DESCRIZIONE] [varchar](1000) NULL,
	[SIGLA] [varchar](2) NULL,
	[CODICEPROVINCIA] [varchar](3) NULL,
	[REGIONE] [int] NULL,
	CONSTRAINT [FK_Provincia_Regione] FOREIGN KEY ([Regione]) REFERENCES [dbo].[Regione] ([Oid]), 
    CONSTRAINT [PK_Provincia] PRIMARY KEY ([OID])
)
