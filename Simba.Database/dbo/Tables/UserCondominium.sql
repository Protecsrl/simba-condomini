CREATE TABLE [dbo].[UserCondominium] (
    [IdUser]        INT NOT NULL,
    [IdCondominium] INT NOT NULL,
    [Oid]           INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_UserCondominium_1] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_UserCondominium_Condominium] FOREIGN KEY ([IdCondominium]) REFERENCES [dbo].[Condominium] ([Oid]),
    CONSTRAINT [FK_UserCondominium_User] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([Oid])
);



