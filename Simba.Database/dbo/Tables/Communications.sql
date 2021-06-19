CREATE TABLE [dbo].[Communications] (
    [Oid]                 INT           IDENTITY (1, 1) NOT NULL,
    [Number]              INT           NULL,
    [Titolo]               NVARCHAR (50) NOT NULL,
    [ParentCommunication] INT           NULL,
    [User]                INT           NOT NULL,
    [Condominium]         INT           NULL,
    [DateInsert]          DATETIME2 (7) NULL,
    [Type]                INT           NULL,
    [Descrizione] NVARCHAR(500) NOT NULL, 
    [Note] NVARCHAR(500) NULL, 
    CONSTRAINT [PK_Communications] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Communications_CommunicationType] FOREIGN KEY ([Type]) REFERENCES [dbo].[CommunicationType] ([Oid]),
    CONSTRAINT [FK_Communications_Condominium] FOREIGN KEY ([Condominium]) REFERENCES [dbo].[Condominium] ([Oid]),
    CONSTRAINT [FK_Communications_User] FOREIGN KEY ([User]) REFERENCES [dbo].[User] ([Oid])
);



