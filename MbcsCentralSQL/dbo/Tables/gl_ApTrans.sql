CREATE TABLE [dbo].[gl_ApTrans] (
    [BatchNumber] INT           NULL,
    [Key]         INT           IDENTITY (1, 1) NOT NULL,
    [Company]     SMALLINT      NULL,
    [Department]  SMALLINT      NULL,
    [Account]     INT           NULL,
    [Date]        DATETIME2 (7) NULL,
    [Description] NVARCHAR (60) NULL,
    [Amount]      MONEY         NULL,
    [Journal]     NVARCHAR (20) NULL,
    [CheckNumber] INT           NULL,
    CONSTRAINT [ApTrans$PrimaryKey] PRIMARY KEY CLUSTERED ([Key] ASC)
);

