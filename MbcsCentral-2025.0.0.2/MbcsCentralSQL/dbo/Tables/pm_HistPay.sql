CREATE TABLE [dbo].[pm_HistPay] (
    [Building Number] NVARCHAR (3) NULL,
    [Tenant Number]   NVARCHAR (4) NULL,
    [Sequence Number] INT          NULL,
    [Date]            DATETIME     NULL,
    [Amount]          MONEY        NULL,
    [Code]            NVARCHAR (1) NULL,
    [hist_pay_id]     INT          IDENTITY (1, 1) NOT NULL,
    [clientId]        INT          NOT NULL,
     [Notes] VARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_pm_HistPay] PRIMARY KEY CLUSTERED ([hist_pay_id] ASC)
);

