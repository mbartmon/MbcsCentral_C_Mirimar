CREATE TABLE [dbo].[gl_AccountRanges] (
    [ReportName]    NVARCHAR (32) NULL,
    [ReportSection] INT           NULL,
    [AccountNumber] INT           NULL,
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [AccountRanges$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

