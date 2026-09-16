ALTER TABLE [dbo].[pm_Tenants]
    ADD [dhcrSubmittedOne]       BIT           DEFAULT 0 NOT NULL,
        [dhcrSubmittedTwo]       BIT           DEFAULT 0 NOT NULL,
        [dhcrSubmittedDateOne]   DATETIME2 (7) NULL,
        [dhcrSubmittedDateTwo]   DATETIME2 (7) NULL,
        [dhcrInformedConsentOne] BIT           DEFAULT 0 NOT NULL,
        [dhcrInformedConsentTwo] BIT           DEFAULT 0 NOT NULL,
        [dhcrTotalCostOne]       MONEY         DEFAULT 0 NOT NULL,
        [dhcrTotalCostTwo]       MONEY         DEFAULT 0 NOT NULL;
GO

ALTER TABLE [dbo].[pm_TenantsArchive]
    ADD [DecontrolledLegal]      MONEY         NULL,
        [LatePercent]            BIT           DEFAULT 0 NOT NULL,
        [dhcrSubmittedOne]       BIT           DEFAULT 0 NOT NULL,
        [dhcrSubmittedTwo]       BIT           DEFAULT 0 NOT NULL,
        [dhcrSubmittedDateOne]   DATETIME2 (7) NULL,
        [dhcrSubmittedDateTwo]   DATETIME2 (7) NULL,
        [dhcrInformedConsentOne] BIT           DEFAULT 0 NOT NULL,
        [dhcrInformedConsentTwo] BIT           DEFAULT 0 NOT NULL,
        [dhcrTotalCostOne]       MONEY         DEFAULT 0 NOT NULL,
        [dhcrTotalCostTwo]       MONEY         DEFAULT 0 NOT NULL;
GO
