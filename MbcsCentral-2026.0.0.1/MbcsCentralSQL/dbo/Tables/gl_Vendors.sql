CREATE TABLE [dbo].[gl_Vendors] (
    [VendorNumber]       INT            IDENTITY (1, 1) NOT NULL,
    [VendorName]         NVARCHAR (50)  NULL,
    [Name2]              NVARCHAR (50)  NULL,
    [Street]             NVARCHAR (50)  NULL,
    [City]               NVARCHAR (50)  NULL,
    [State]              NVARCHAR (2)   NULL,
    [Zip]                NVARCHAR (10)  NULL,
    [Telephone]          NVARCHAR (20)  NULL,
    [Fax]                NVARCHAR (20)  NULL,
    [Email]              NVARCHAR (50)  NULL,
    [Payroll]            BIT            NULL,
    [FederalID]          NVARCHAR (11)  NULL,
    [StateID]            NVARCHAR (11)  NULL,
    [Corporation]        BIT            NULL,
    [CurrentPurchases]   MONEY          NULL,
    [Journal]            NVARCHAR (20)  NULL,
    [PriorYearPurchases] MONEY          NULL,
    [OldNumber]          NVARCHAR (4)   NULL,
    [Comment]            NVARCHAR (128) NULL,
    [Inactive]           BIT            NULL,
    CONSTRAINT [Vendors$PrimaryKey] PRIMARY KEY CLUSTERED ([VendorNumber] ASC)
);

