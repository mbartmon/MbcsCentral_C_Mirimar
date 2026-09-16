CREATE TABLE [dbo].[gl_ApChecks] (
    [Company]        SMALLINT      NULL,
    [Department]     SMALLINT      NULL,
    [CheckNumber]    INT           NULL,
    [Sequence]       SMALLINT      NULL,
    [Date]           DATETIME2 (7) NULL,
    [Amount]         MONEY         NULL,
    [DistCompany]    SMALLINT      NULL,
    [DistDepartment] SMALLINT      NULL,
    [Vendor/Account] INT           NULL,
    [ReconFlag]      BIT           NULL,
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [Journal]        NCHAR (20)    NOT NULL,
    CONSTRAINT [ApChecks$PrimaryKey] PRIMARY KEY CLUSTERED ([ID] ASC)
);

