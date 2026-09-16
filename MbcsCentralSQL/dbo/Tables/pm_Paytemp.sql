CREATE TABLE [dbo].[pm_Paytemp] (
    [UserID]          NVARCHAR (18) NULL,
    [Building Number] NVARCHAR (3)  NULL,
    [Tenant Number]   NVARCHAR (4)  NULL,
    [Date]            DATETIME      NULL,
    [Amount]          MONEY         NULL,
    [Code]            NVARCHAR (2)  NULL,
    [Name]            NVARCHAR (30) NULL,
    [Apartment]       NVARCHAR (4)  NULL,
    [clientId]        INT           NOT NULL,
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Notes] VARCHAR(200) NOT NULL, 
    CONSTRAINT [PK_pm_Paytemp] PRIMARY KEY CLUSTERED ([ID] ASC)
);

