CREATE TABLE [dbo].[gl_BatchReports] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [BatchKey]   INT           NULL,
    [ReportType] NVARCHAR (2)  NULL,
    [Report]     VARCHAR (MAX) NOT NULL,
    CONSTRAINT [BatchReports$PrimaryKey] PRIMARY KEY CLUSTERED ([ID] ASC)
);

