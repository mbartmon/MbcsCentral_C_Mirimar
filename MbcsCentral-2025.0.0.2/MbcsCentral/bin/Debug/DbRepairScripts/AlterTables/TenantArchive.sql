ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantsArchive_CLientId_Default DEFAULt 1 FOR ClientId;
GO

ALTER TABLE [dbo].[pm_TenantsArchive]
    ADD [UseACH]     BIT DEFAULT 0 NOT NULL,
        [PaymentDay] INT DEFAULT 1 NOT NULL,
	[SavingsAccount] BIT DEFAULT 0 NOT NULL;

GO

