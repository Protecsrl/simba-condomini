CREATE TABLE [dbo].[CommunicationType] (
    [Oid]         INT            IDENTITY (1, 1) NOT NULL,
    [Nome]        NVARCHAR (50)  NOT NULL,
    [Descrizione] NVARCHAR (500) NULL,
    CONSTRAINT [PK_CommunicationType] PRIMARY KEY CLUSTERED ([Oid] ASC)
);

