CREATE TABLE [dbo].[pm_TenantsSnapShot] (
    [clientId]          INT           NOT NULL,
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [Year]              INT           NOT NULL,
    [Month]             INT           NOT NULL,
    [Sequence Number]   INT           NOT NULL,
    [Building Number]   VARCHAR (3)   NOT NULL,
    [Tenant Number]     VARCHAR (4)   NOT NULL,
    [Tenant Name]       VARCHAR (30)  NOT NULL,
    [Apartment]         VARCHAR (4)   NOT NULL,
    [Rent]              MONEY         NOT NULL,
    [Security]          MONEY         NOT NULL,
    [Arrears]           MONEY         NOT NULL,
    [Paid]              MONEY         NOT NULL,
    [Last Paid Date]    DATETIME2 (7) NOT NULL,
    [Total Charges]     MONEY         NOT NULL,
    [RentLegal]         MONEY         NOT NULL,
    [DecontrolledLegal] MONEY         NOT NULL,
    CONSTRAINT [pm_TenantsSnapShot$PrimaryKey] PRIMARY KEY CLUSTERED ([ID] ASC)
);

