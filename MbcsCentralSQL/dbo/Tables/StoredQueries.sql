CREATE TABLE [dbo].[StoredQueries] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Parameters] VARCHAR (MAX) NULL,
    [Query]      VARCHAR (MAX) NULL,
    CONSTRAINT [PK_StoredQueries] PRIMARY KEY CLUSTERED ([ID] ASC)
);

