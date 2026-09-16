CREATE TABLE [dbo].[gl_JournalNames] (
    [JournalName] NVARCHAR (20) NULL,
    [Description] NVARCHAR (50) NULL,
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [JournalNames$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

