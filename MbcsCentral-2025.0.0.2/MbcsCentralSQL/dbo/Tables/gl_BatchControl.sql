CREATE TABLE [dbo].[gl_BatchControl] (
    [Key]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (30) NULL,
    [LastDate]      DATETIME2 (7) NULL,
    [InvoiceOption] SMALLINT      NULL,
    [Comment]       NCHAR (64)    NOT NULL,
    CONSTRAINT [BatchControl$PrimaryKey] PRIMARY KEY CLUSTERED ([Key] ASC)
);

