CREATE TABLE [dbo].[TicketStatuses] (
    [IdTicket]    INT            NOT NULL,
    [IdStatus]    INT            NOT NULL,
    [Data]        DATETIME       NOT NULL,
    [IdUser]      INT            NOT NULL,
    [Oid]         INT            IDENTITY (1, 1) NOT NULL,
    [Descrizione] NVARCHAR (100) NULL,
    CONSTRAINT [PK_TicketStatuses_1] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_TicketStatuses_Ticket] FOREIGN KEY ([IdTicket]) REFERENCES [dbo].[Ticket] ([Oid]),
    CONSTRAINT [FK_TicketStatuses_TicketStatus] FOREIGN KEY ([IdStatus]) REFERENCES [dbo].[TicketStatus] ([Oid]),
    CONSTRAINT [FK_TicketStatuses_User] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[User] ([Oid])
);



