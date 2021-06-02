CREATE TABLE [dbo].[UserType] (
    [Oid]  INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED ([Oid] ASC)
);

