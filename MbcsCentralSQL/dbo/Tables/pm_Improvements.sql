CREATE TABLE [dbo].[pm_Improvements] (
    [Building Number] VARCHAR (3)   NOT NULL,
    [Tenant Number]   VARCHAR (3)   NOT NULL,
    [Date]            DATETIME2 (7) NOT NULL,
    [Line Number]     INT           IDENTITY (1, 1) NOT NULL,
    [Type]            VARCHAR (16)  NULL,
    [Brand]           VARCHAR (15)  NULL,
    [model]           VARCHAR (15)  NULL,
    [size]            VARCHAR (10)  NULL,
    [Color]           VARCHAR (10)  NULL,
    [Vendor]          VARCHAR (15)  NULL,
    [Cost]            MONEY         NULL,
    [Battery]         VARCHAR (18)  NULL,
    [clientId]        INT           NOT NULL,
    [RentIncreaseAmount] MONEY NOT NULL DEFAULT 0, 
    [RentIncreaseDate] DATETIME2 NOT NULL DEFAULT '01/01/1900', 
    CONSTRAINT [PK_Improvements] PRIMARY KEY CLUSTERED ([Line Number] ASC)
);

