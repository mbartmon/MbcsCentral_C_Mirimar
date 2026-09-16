ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_LatePercent_Default DEFAULT 0 FOR LatePercent;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_PaymentDay_Default DEFAULT 0 FOR PaymentDay;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_SavingsAccount_Default DEFAULT 0 FOR SavingsAccount;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_UseACH_Default DEFAULT 0 FOR UseACH;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_ClientID_Default DEFAULT 1 for ClientID;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_dhcrSubmittedOne_Default DEFAULT 0 for dhcrSubmittedOne;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_dhcrSubmittedTwo_Default DEFAULT 0 for dhcrSubmittedTwo;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_dhcrInformedConsentOne_Default DEFAULT 0 for dhcrInformedConsentOne;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_dhcrInformedConsentTwo_Default DEFAULT 0 for dhcrInformedConsentTwo;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_dhcrTotalCostOne_Default DEFAULT 0 for dhcrTotalCostOne;
GO
ALTER TABLE pm_TenantsArchive ADD CONSTRAINT TenantArchive_dhcrTotalCostTwo_Default DEFAULT 0 for dhcrTotalCostTwo;
GO