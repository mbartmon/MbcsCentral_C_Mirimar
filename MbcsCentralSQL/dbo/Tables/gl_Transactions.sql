CREATE TABLE [dbo].[gl_Transactions] (
    [CompanyNumber]    SMALLINT       NULL,
    [DepartmentNumber] SMALLINT       NULL,
    [AccountNumber]    INT            NULL,
    [FiscalYear]       SMALLINT       NULL,
    [Date]             DATETIME2 (7)  NULL,
    [IndexNumber]      INT            IDENTITY (1, 1) NOT NULL,
    [Journal]          NVARCHAR (32)  NULL,
    [Source]           NVARCHAR (32)  NULL,
    [VendorNumber]     NVARCHAR (50)  NULL,
    [CheckNumber]      INT            NULL,
    [Amount]           MONEY          NULL,
    [MajorCategory]    SMALLINT       NULL,
    [MinorCategory]    SMALLINT       NULL,
    [RecFlag]          BIT            NULL,
    [Description]      NVARCHAR (128) NULL,
    CONSTRAINT [Transactions$PrimaryKey] PRIMARY KEY CLUSTERED ([IndexNumber] ASC)
);

