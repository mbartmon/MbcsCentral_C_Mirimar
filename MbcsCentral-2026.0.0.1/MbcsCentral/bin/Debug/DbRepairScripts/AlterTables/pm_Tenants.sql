ALTER TABLE [dbo].[pm_Tenants]
	ADD [DirectPay] BIT DEFAULT 0 NOT NULL;
GO
ALTER TABLE [dbo].[pm_Tenants]
    ADD [UseACH]     BIT DEFAULT 0 NOT NULL,
        [PaymentDay] INT DEFAULT 1 NOT NULL,
	[SavingsAccount] BIT DEFAULT 0 NOT NULL;
GO
ALTER TABLE [dbo].[pm_Tenants]
    ADD [LatePercent] BIT DEFAULT 0 NOT NULL;
GO

