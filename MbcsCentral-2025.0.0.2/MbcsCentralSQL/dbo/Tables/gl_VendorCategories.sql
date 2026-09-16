CREATE TABLE [dbo].[gl_VendorCategories] (
    [VendorNumber] INT           NULL,
    [Company]      SMALLINT      NULL,
    [Department]   SMALLINT      NULL,
    [Account]      INT           NULL,
    [Amount]       MONEY         NULL,
    [JournalName]  NVARCHAR (20) NULL,
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Order]        SMALLINT      NOT NULL,
    CONSTRAINT [VendorCategories$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

