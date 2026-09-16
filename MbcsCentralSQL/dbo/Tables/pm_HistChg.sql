CREATE TABLE [dbo].[pm_HistChg] (
    [Building Number] NVARCHAR (3) NULL,
    [Tenant Number]   NVARCHAR (4) NULL,
    [Sequence Number] INT          NULL,
    [Charge Number]   INT          NULL,
    [Date]            DATETIME     NULL,
    [Code]            NVARCHAR (2) NULL,
    [Amount]          MONEY        NULL,
    [Arrears]         MONEY        NULL,
    [hist_chg_id]     INT          IDENTITY (1, 1) NOT NULL,
    [clientId]        INT          NOT NULL,
    CONSTRAINT [PK_pm_HistChg] PRIMARY KEY CLUSTERED ([hist_chg_id] ASC)
);

