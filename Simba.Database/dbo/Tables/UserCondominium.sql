CREATE TABLE [dbo].[UserCondominium] (
    [IdUser]        INT NOT NULL,
    [IdCondominium] INT NOT NULL,
    CONSTRAINT [PK_UserCondominium] PRIMARY KEY CLUSTERED ([IdUser] ASC, [IdCondominium] ASC),
    CONSTRAINT [FK_UserCondominium_Condominium] FOREIGN KEY ([IdCondominium]) REFERENCES [dbo].[Condominium] ([Oid]),
    CONSTRAINT [FK_UserCondominium_User] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([Oid])
);

