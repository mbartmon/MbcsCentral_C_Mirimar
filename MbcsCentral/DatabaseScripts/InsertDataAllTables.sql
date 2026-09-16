SET NOCOUNT ON
GO

PRINT 'Inserting data for all tables'
GO

:On Error exit

:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.Accounts.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.ApplicationAssignments.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.Applications.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.Classes.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.Clients.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.DocumentCategories.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.DocumentLayoutFieldMaster.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.DocumentLayoutFields.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.DocumentLayoutList.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.do_Documents.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.ExternalApplications.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_AccountRanges.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Accounts.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_ApChecks.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_ApTrans.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Balances.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_BatchControl.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_BatchJrnEntries.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_BatchJrnHeader.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_BatchRptSave.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Budgets.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Category.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_CheckNumbers.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Company.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Control.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Department.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_InvBatchDistributions.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_InvoiceDistributions.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_InvoiceHistory.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Invoices.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_InvoicesBatch.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_JournalNames.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Journals.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Memos.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_OpStmt.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_PRaccounts.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Transactions.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Transactiontemp.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_VendorCategories.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.gl_Vendors.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.GroupMembers.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.groups.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.group_permissions.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.parameters.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.permission.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Building.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_ChargeCodes.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Charges.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Control.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_FutureRents.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_GLTable.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_HistChg.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Histmast.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_HistPay.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Improvements.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Legal.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_LegalMsg.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Maintenance.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Occupants.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Owner.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Payments.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Paytemp.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_StatGroups.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_STCodes.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_STPeriods.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_STRates.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_STTenants.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_STTrans.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Tenants.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_TenantsArchive.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_TenantsSnapShot.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_TypeMsg.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.pm_Types.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.Registry.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.sysdiagrams.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.Test.Table.sql
:r c:\users\Mike\Documents\SQL_2015_10_08\dbo.user_permissions.Table.sql

PRINT 'Data for All Tables inserted.'
GO