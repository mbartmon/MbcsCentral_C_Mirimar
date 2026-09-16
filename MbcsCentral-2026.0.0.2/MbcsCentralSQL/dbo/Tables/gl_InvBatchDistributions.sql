CREATE TABLE [dbo].[gl_InvBatchDistributions] (
    [InvoiceKey] INT           NULL,
    [Company]    SMALLINT      NULL,
    [Department] SMALLINT      NULL,
    [Account]    INT           NULL,
    [Amount]     MONEY         NULL,
    [Journal]    NVARCHAR (20) NULL,
    [Batch]      INT           NULL,
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [InvBatchDistributions$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

