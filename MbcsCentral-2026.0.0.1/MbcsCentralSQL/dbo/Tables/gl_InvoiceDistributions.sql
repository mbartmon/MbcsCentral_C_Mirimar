CREATE TABLE [dbo].[gl_InvoiceDistributions] (
    [InvoiceKey] INT           NULL,
    [Company]    INT           NULL,
    [Department] INT           NULL,
    [Account]    INT           NULL,
    [Amount]     MONEY         NULL,
    [Journal]    NVARCHAR (20) NULL,
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Batch]      INT           NOT NULL,
    CONSTRAINT [InvoiceDistributions$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

