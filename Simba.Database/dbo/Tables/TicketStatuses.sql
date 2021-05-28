CREATE TABLE [dbo].[TicketStatuses] (
    [IdTicket] INT      NOT NULL,
    [IdStatus] INT      NOT NULL,
    [Data]     DATETIME NOT NULL,
    [IdUser]   INT      NOT NULL,
    CONSTRAINT [PK_TicketStatuses] PRIMARY KEY CLUSTERED ([IdTicket] ASC, [IdStatus] ASC),
    CONSTRAINT [FK_TicketStatuses_Ticket] FOREIGN KEY ([IdTicket]) REFERENCES [dbo].[Ticket] ([Oid]),
    CONSTRAINT [FK_TicketStatuses_TicketStatus] FOREIGN KEY ([IdStatus]) REFERENCES [dbo].[TicketStatus] ([Oid]),
    CONSTRAINT [FK_TicketStatuses_User] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([Oid])
);

