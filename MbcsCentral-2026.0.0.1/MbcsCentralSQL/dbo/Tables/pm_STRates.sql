CREATE TABLE [dbo].[pm_STRates] (
    [Building Number] NVARCHAR (3) NULL,
    [Date]            DATETIME     NULL,
    [Rate]            REAL         NULL,
    [clientId]        INT          NOT NULL,
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_STRates] PRIMARY KEY CLUSTERED ([ID] ASC)
);

