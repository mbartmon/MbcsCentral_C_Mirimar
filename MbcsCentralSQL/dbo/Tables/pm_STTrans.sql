CREATE TABLE [dbo].[pm_STTrans] (
    [Building Number] NVARCHAR (3) NULL,
    [Tenant Number]   NVARCHAR (4) NULL,
    [Sequence Number] INT          NULL,
    [Trans Type]      INT          NULL,
    [Date]            DATETIME     NULL,
    [Amount]          MONEY        NULL,
    [clientId]        INT          NOT NULL,
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_STTrans] PRIMARY KEY CLUSTERED ([ID] ASC)
);

