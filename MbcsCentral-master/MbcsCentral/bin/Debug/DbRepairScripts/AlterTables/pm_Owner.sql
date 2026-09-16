ALTER TABLE [dbo].[pm_Owner]
    ADD [BankRoutingNumber] NCHAR (9) DEFAULT ' ' NOT NULL;
GO
ALTER TABLE [dbo].[pm_Owner]
    ADD [BankAccountNumber] NCHAR (17) DEFAULT ' ' NOT NULL;
GO
