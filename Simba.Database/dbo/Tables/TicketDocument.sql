CREATE TABLE [dbo].[TicketDocument] (
    [IdTicket]   INT           NOT NULL,
    [IdDocument] INT           NOT NULL,
    [Date]       DATETIME2 (7) NULL,
    [UserId]     INT           NULL,
    [Oid]        INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_TicketDocument_1] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_TicketDocument_Documents] FOREIGN KEY ([IdDocument]) REFERENCES [dbo].[Documents] ([Oid]),
    CONSTRAINT [FK_TicketDocument_Ticket] FOREIGN KEY ([IdTicket]) REFERENCES [dbo].[Ticket] ([Oid]),
    CONSTRAINT [FK_TicketDocument_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Oid])
);



