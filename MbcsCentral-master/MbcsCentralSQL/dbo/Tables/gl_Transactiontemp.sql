CREATE TABLE [dbo].[gl_Transactiontemp] (
    [CompanyNumber]    SMALLINT      NULL,
    [DepartmentNumber] SMALLINT      NULL,
    [AccountNumber]    INT           NULL,
    [FiscalYear]       SMALLINT      NULL,
    [Date]             DATETIME2 (7) NULL,
    [IndexNumber]      INT           IDENTITY (1, 1) NOT NULL,
    [Journal]          NVARCHAR (32) NULL,
    [Source]           NVARCHAR (32) NULL,
    [Description]      NVARCHAR (60) NULL,
    [VendorNumber]     NVARCHAR (50) NULL,
    [CheckNumber]      INT           NULL,
    [Amount]           MONEY         NULL,
    [MajorCategory]    SMALLINT      NULL,
    [MinorCategory]    SMALLINT      NULL,
    CONSTRAINT [Transactiontemp$PrimaryKey] PRIMARY KEY CLUSTERED ([IndexNumber] ASC)
);

