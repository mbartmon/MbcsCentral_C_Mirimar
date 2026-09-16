CREATE TABLE [dbo].[gl_OpStmt] (
    [ReportName] NVARCHAR (32) NULL,
    [Detail]     SMALLINT      NULL,
    [Current]    SMALLINT      NULL,
    [Prior]      SMALLINT      NULL,
    [Percent]    SMALLINT      NULL,
    [YTD]        SMALLINT      NULL,
    [Zero]       SMALLINT      NULL,
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [OpStmt$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

