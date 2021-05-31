CREATE TABLE [dbo].[User] (
    [Oid]         INT           IDENTITY (1, 1) NOT NULL,
    [Username]    NVARCHAR (50) NOT NULL,
    [Password]    NVARCHAR (50) NOT NULL,
    [UserType]    INT           NOT NULL,
    [Nome]        NVARCHAR (50) NOT NULL,
    [Congnome]    NVARCHAR (50) NOT NULL,
    [Scala]       INT           NULL,
    [Environment]     INT           NULL,
    [Azienda]     INT           NULL,
    [Latitudine]  FLOAT (53)    NULL,
    [Longitudine] FLOAT (53)    NULL,
    [DateInsert]  DATETIME2 (7) NOT NULL,
    [DateUpdate]  DATETIME2 (7) NULL,
    [Building]    INT           NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_User_Building] FOREIGN KEY ([Building]) REFERENCES [dbo].[Building] ([Id]),
    CONSTRAINT [FK_User_UserType] FOREIGN KEY ([UserType]) REFERENCES [dbo].[UserType] ([Oid]),
    CONSTRAINT [FK_User_Envinronment] FOREIGN KEY ([Environment]) REFERENCES [dbo].[Environment] ([Oid])
);

