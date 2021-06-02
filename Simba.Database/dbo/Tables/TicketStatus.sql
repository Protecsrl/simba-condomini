CREATE TABLE [dbo].[TicketStatus] (
    [Oid]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Descrizione] NVARCHAR (500) NULL,
    CONSTRAINT [PK_TicketStatus] PRIMARY KEY CLUSTERED ([Oid] ASC)
);

