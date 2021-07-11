CREATE TABLE [dbo].[Regione]
(
	[Oid] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CodiceRegione] [varchar](2) NULL,
	[Descrizione] [varchar](1000) NULL,
	[Nazione] [varchar](500) NULL, 
    CONSTRAINT [PK_Regione] PRIMARY KEY ([Oid])
)
