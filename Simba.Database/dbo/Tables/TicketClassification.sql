CREATE TABLE [dbo].[TicketClassification] (
    [Oid]         INT            IDENTITY (1, 1) NOT NULL,
    [Nome]        NVARCHAR (50)  NOT NULL,
    [Descrizione] NVARCHAR (500) NULL,
    CONSTRAINT [PK_TicketClassification] PRIMARY KEY CLUSTERED ([Oid] ASC)
);

