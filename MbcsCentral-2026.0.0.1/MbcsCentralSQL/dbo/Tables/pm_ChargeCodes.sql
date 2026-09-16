CREATE TABLE [dbo].[pm_ChargeCodes] (
    [Charge Code]           NVARCHAR (2)  NOT NULL,
    [Charge Description]    NVARCHAR (32) NULL,
    [GL Credit Account]     NVARCHAR (4)  NULL,
    [GL Credit Sub-Account] NVARCHAR (3)  NULL,
    [GL Debit Account]      NVARCHAR (4)  NULL,
    [GL Debit Sub-Account]  NVARCHAR (3)  NULL,
    [GL Cash Account]       NVARCHAR (4)  NULL,
    [Gl Cash Sub-Account]   NVARCHAR (3)  NULL,
    [Legal Code]            BIT           NOT NULL,
    [rentFlag]              BIT           NOT NULL,
    [securityFlag]          BIT           NOT NULL,
    [clientId]              INT           NOT NULL,
    [ID]                    INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_ChargeCodes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

