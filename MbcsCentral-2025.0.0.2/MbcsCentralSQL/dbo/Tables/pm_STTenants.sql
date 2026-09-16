CREATE TABLE [dbo].[pm_STTenants] (
    [Building Number]     NVARCHAR (3)  NOT NULL,
    [Tenant Number]       NVARCHAR (4)  NOT NULL,
    [Sequence Number]     INT           NOT NULL,
    [Apartment]           NVARCHAR (4)  NULL,
    [Tenant Name]         NVARCHAR (64) NULL,
    [Amount]              MONEY         NULL,
    [Social Security One] NVARCHAR (11) NULL,
    [Social Security Two] NVARCHAR (11) NULL,
    [Interest]            NVARCHAR (1)  NULL,
    [clientId]            INT           NOT NULL,
    [ID]                  INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_STTenants] PRIMARY KEY CLUSTERED ([ID] ASC)
);

