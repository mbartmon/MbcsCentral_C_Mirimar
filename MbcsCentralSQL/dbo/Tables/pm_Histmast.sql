CREATE TABLE [dbo].[pm_Histmast] (
    [Building Number] NVARCHAR (3)  NOT NULL,
    [Tenant Number]   NVARCHAR (4)  NOT NULL,
    [Sequence Number] INT           NOT NULL,
    [Tenant Name]     NVARCHAR (30) NULL,
    [Apartment]       NVARCHAR (4)  NULL,
    [clientId]        INT           NOT NULL,
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_Histmast] PRIMARY KEY CLUSTERED ([ID] ASC)
);

