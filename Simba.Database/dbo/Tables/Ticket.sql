CREATE TABLE [dbo].[Ticket] (
    [Oid]          INT           IDENTITY (1, 1) NOT NULL,
    [Number]       INT           NOT NULL,
    [TicketStatus] INT           NULL,
    [Data]         DATETIME2 (7) NULL,
    [Note]         VARCHAR (MAX) NOT NULL,
    [Rating]       SMALLINT      NULL,
    [DateCreation] DATETIME      NOT NULL,
    [DateUpdate]   DATETIME      NULL,
    [User]         INT           NOT NULL,
    [Condominium]  INT           NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Ticket_Condominium] FOREIGN KEY ([Condominium]) REFERENCES [dbo].[Condominium] ([Oid]),
    CONSTRAINT [FK_Ticket_User] FOREIGN KEY ([User]) REFERENCES [dbo].[User] ([Oid])
);

