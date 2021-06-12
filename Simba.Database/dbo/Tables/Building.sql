CREATE TABLE [dbo].[Building] (
    [Oid]         INT           IDENTITY (1, 1) NOT NULL,
    [Nome]        NVARCHAR (50) NULL,
    [Condominium] INT           NULL,
    CONSTRAINT [PK_Stair] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Building_Condominium] FOREIGN KEY ([Condominium]) REFERENCES [dbo].[Condominium] ([Oid])
);



