CREATE TABLE [dbo].[pm_Payments] (
    [Building Number] NVARCHAR (3) NULL,
    [Tenant Number]   NVARCHAR (4) NULL,
    [Date of Entry]   DATETIME     NULL,
    [Amount]          MONEY        NULL,
    [Security Code]   NVARCHAR (1) NULL,
    [Sequence Number] INT          NULL,
    [clientId]        INT          NOT NULL,
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
     [Notes] VARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_pm_Payments] PRIMARY KEY CLUSTERED ([ID] ASC)
);

