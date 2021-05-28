CREATE TABLE [dbo].[Documents] (
    [Oid]  INT          IDENTITY (1, 1) NOT NULL,
    [path] VARCHAR (50) NULL,
    [type] VARCHAR (50) NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([Oid] ASC)
);

