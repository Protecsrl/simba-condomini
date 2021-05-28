CREATE TABLE [dbo].[TicketDocument] (
    [IdTicket]   INT           NOT NULL,
    [IdDocument] INT           NOT NULL,
    [Date]       DATETIME2 (7) NULL,
    [UserId]     INT           NULL,
    CONSTRAINT [PK_TicketDocument] PRIMARY KEY CLUSTERED ([IdTicket] ASC, [IdDocument] ASC),
    CONSTRAINT [FK_TicketDocument_Documents] FOREIGN KEY ([IdDocument]) REFERENCES [dbo].[Documents] ([Oid]),
    CONSTRAINT [FK_TicketDocument_Ticket] FOREIGN KEY ([IdTicket]) REFERENCES [dbo].[Ticket] ([Oid]),
    CONSTRAINT [FK_TicketDocument_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Oid])
);

