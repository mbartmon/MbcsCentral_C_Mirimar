CREATE TABLE [dbo].[pm_Maintenance] (
    [Building Number] VARCHAR (3)   NOT NULL,
    [Tenant Number]   VARCHAR (3)   NOT NULL,
    [Date]            DATETIME2 (7) NOT NULL,
    [Line Number]     INT           IDENTITY (1, 1) NOT NULL,
    [Description]     VARCHAR (MAX) NULL,
    [Vendor]          VARCHAR (15)  NULL,
    [Cost]            MONEY         NULL,
    [Inspected]       BIT           NULL,
    [clientId]        INT           NOT NULL,
    [XrfTested] BIT NOT NULL DEFAULT 0, 
    [XrfPassed] BIT NOT NULL DEFAULT 0, 
    [XrfFailed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [Maintenance$PrimaryKey] PRIMARY KEY CLUSTERED ([Building Number] ASC, [Tenant Number] ASC, [Date] ASC, [Line Number] ASC)
);

