CREATE TABLE [dbo].[TicketSuplliers]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[IdTicket] INT NOT NULL,
	[IdSuplier] INT NOT NULL,
	[DateAdded] DateTime NOT NULL,
	[User] Int Not Null,
    CONSTRAINT FK_TicketSuplliers_Supplier FOREIGN KEY ([IdSuplier]) REFERENCES [dbo].[User] ([Oid]),
    CONSTRAINT FK_TicketSuplliers_User FOREIGN KEY ([User]) REFERENCES [dbo].[User] ([Oid]),
    CONSTRAINT FK_TicketSuplliers_Ticket FOREIGN KEY ([IdTicket]) REFERENCES [dbo].[Ticket] ([Oid])
)
