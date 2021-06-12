CREATE TABLE [dbo].[Environment] (
    [Oid]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [Building]    INT            NOT NULL,
    [Valid]       BIT            NOT NULL,
    CONSTRAINT [PK_Environment] PRIMARY KEY CLUSTERED ([Oid] ASC),
    CONSTRAINT [FK_Environment_Building] FOREIGN KEY ([Building]) REFERENCES [dbo].[Building] ([Oid])
);



