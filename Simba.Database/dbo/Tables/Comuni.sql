CREATE TABLE [dbo].[Comuni]
(
	[Oid] INT NOT NULL PRIMARY KEY,
	[RipartizioneGeografica] [varchar](1000) NULL,
	[CodiceNuts22010] [varchar](1000) NULL,
	[CodiceNuts32010] [varchar](1000) NULL,
	[CodiceRegione] [varchar](1000) NULL,
	[CodiceProvincia] [varchar](1000) NULL,
	[CodiceCittaMetropolitana] [varchar](1000) NULL,
	[NumProgressivoComune] [varchar](1000) NULL,
	[CodiceIstat] [varchar](1000) NULL,
	[CodiceIstatNum] [varchar](1000) NULL,
	[CodiceIstat107Prov] [varchar](1000) NULL,
	[CodiceIstat103Prov] [varchar](1000) NULL,
	[CodiceCatastale] [varchar](1000) NULL,
	[DenominazioneItaliano] [varchar](1000) NULL,
	[CapoluogoDiProvincia] [varchar](1000) NULL,
	[ZonaAltimetrica] [varchar](1000) NULL,
	[AltitudineDelCentro] [varchar](1000) NULL,
	[ComuneLitoraneo] [varchar](1000) NULL,
	[ComuneMontano] [varchar](1000) NULL,
	[SuperficieTerritoriale] [varchar](1000) NULL,
	[Popolazione2001] [varchar](1000) NULL,
	[Popolazione2011] [varchar](1000) NULL,
	[Provincia] [int] NULL, 
    CONSTRAINT [FK_Comuni_Provincia] FOREIGN KEY ([Provincia]) REFERENCES [PROVINCIA]([Oid])
	
)


