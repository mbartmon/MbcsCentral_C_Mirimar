CREATE TABLE [dbo].[gl_Company] (
    [CompanyNumber]     SMALLINT      NOT NULL,
    [Name]              NVARCHAR (50) NULL,
    [StreetAddress]     NVARCHAR (50) NULL,
    [City]              NVARCHAR (50) NULL,
    [State]             NVARCHAR (2)  NULL,
    [Zip]               NVARCHAR (10) NULL,
    [Telephone]         NVARCHAR (20) NULL,
    [Fax]               NVARCHAR (20) NULL,
    [Email]             NVARCHAR (50) NULL,
    [FederalID]         NVARCHAR (11) NULL,
    [StateID]           NVARCHAR (11) NULL,
    [FiscalYearEnd]     TINYINT       NULL,
    [CurrentFiscalYear] SMALLINT      NULL,
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [InActive]          BIT           NOT NULL,
    CONSTRAINT [Company$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

