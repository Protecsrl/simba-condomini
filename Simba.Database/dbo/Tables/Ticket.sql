CREATE TABLE [dbo].[Ticket] (
    [Oid]          INT           IDENTITY (1, 1) NOT NULL,
    [Number]       INT           NOT NULL,
    [TicketStatus] INT           NOT NULL,
    [Data]         DATETIME2 (7) NULL,
    [Note]         VARCHAR (MAX) NULL,
    [Rating]       SMALLINT      NULL,
    [DateCreation] DATETIME      NOT NULL,
    [DateUpdate]   DATETIME      NULL,
    [User]         INT           NOT NULL,
    [Condominium]  INT           NULL,
    [Descrizione]  VARCHAR (MAX) NULL,
    [Titolo]       NVARCHAR (50) NULL,
    [Building]     INT           NULL,
    [Enviroment]   INT           NULL,
    [Code]         NVARCHAR (12) NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Ticket_Building] FOREIGN KEY ([Building]) REFERENCES [dbo].[Building] ([Oid]),
    CONSTRAINT [FK_Ticket_Condominium] FOREIGN KEY ([Condominium]) REFERENCES [dbo].[Condominium] ([Oid]),
    CONSTRAINT [FK_Ticket_Environment] FOREIGN KEY ([Enviroment]) REFERENCES [dbo].[Environment] ([Oid]),
    CONSTRAINT [FK_Ticket_User] FOREIGN KEY ([User]) REFERENCES [dbo].[User] ([Oid])
);







