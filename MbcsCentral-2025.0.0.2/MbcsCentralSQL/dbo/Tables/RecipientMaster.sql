CREATE TABLE [dbo].[RecipientMaster] (
    [listId]         INT            IDENTITY (1, 1) NOT NULL,
    [accountId]      INT            NULL,
    [listName]       NVARCHAR (32)  NULL,
    [companyIds]     NVARCHAR (255) NULL,
    [buildingIds]    NVARCHAR (255) NULL,
    [tenantTypes]    NVARCHAR (255) NULL,
    [ownerFilter]    BIT            NULL,
    [buildingFilter] BIT            NULL,
    [typeFilter]     BIT            NULL,
    [arrearsFilter]  BIT            NULL,
    [arrearsAmount]  MONEY          NULL,
    [SSMA_TimeStamp] ROWVERSION     NOT NULL,
    CONSTRAINT [RecipientMaster$PrimaryKey] PRIMARY KEY CLUSTERED ([listId] ASC)
);

