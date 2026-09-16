CREATE TABLE [dbo].[gl_CheckNumbers] (
    [Company]       SMALLINT      NULL,
    [Department]    SMALLINT      NULL,
    [CheckNumber]   INT           NULL,
    [AccountNumber] NVARCHAR (32) NULL,
    [BatchCheck]    INT           NULL,
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [CheckNumbers$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

