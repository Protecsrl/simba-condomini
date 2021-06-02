CREATE TABLE [dbo].[TicketClassifications] (
    [IdTicket]         INT      NOT NULL,
    [IdClassification] INT      NOT NULL,
    [DateInser]        DATETIME NULL,
    [UserId]           INT      NULL,
    [Oid]              INT      NOT NULL,
    CONSTRAINT [PK_TicketClassifications_1] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_TicketClassifications_Ticket] FOREIGN KEY ([IdTicket]) REFERENCES [dbo].[Ticket] ([Oid]),
    CONSTRAINT [FK_TicketClassifications_TicketClassification] FOREIGN KEY ([IdClassification]) REFERENCES [dbo].[TicketClassification] ([Oid]),
    CONSTRAINT [FK_TicketClassifications_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Oid])
);



