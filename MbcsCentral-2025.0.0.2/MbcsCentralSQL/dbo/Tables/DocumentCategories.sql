CREATE TABLE [dbo].[DocumentCategories] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_DocumentCategories] PRIMARY KEY CLUSTERED ([ID] ASC)
);

