ALTER TABLE gl_Memos
ALTER COLUMN Memo NVARCHAR(120)
GO

ALTER TABLE gl_Memos
ADD Keep bit NOT NULL Default (0)
GO

EXEC sp_RENAME 'gl_control.CmpDptMatch', 'ImmediateEftPost', 'COLUMN'
GO

ALTER TABLE [dbo].gl_InvoicesBatch
ADD [EFT] [bit] NULL
GO

ALTER TABLE [dbo].gl_Invoices
ADD [EFT] [bit] NULL
GO

ALTER TABLE [dbo].gl_InvoiceHistory
ADD [EFT] [bit] NULL
GO

ALTER TABLE [dbo].gl_InvoicesBatch
ADD [Prefix] [nchar](6) NOT NULL CONSTRAINT [DF_InvoicesBatch_Prefix]  DEFAULT (''),
	[Suffix] [nchar](12) NOT NULL CONSTRAINT [DF_InvoicesBatch_Suffix] DEFAULT (''),
	[DiscDollar] [bit] NULL
GO

ALTER TABLE [dbo].gl_BatchControl
	ADD [Comment] [nchar](64) NOT NULL CONSTRAINT [DF_BatchControl_Comment] Default ('')

ALTER TABLE [dbo].gl_Invoices
	ADD [DiscDollar] [bit] NULL
GO

ALTER TABLE [dbo].gl_InvoiceHistory
	ADD [DiscDollar] [bit] NULL
GO

ALTER TABLE [dbo].gl_Invoices 
ADD [Prefix] [nchar](6) NOT NULL CONSTRAINT [DF_Invoices_Prefix]  DEFAULT (''),
	[Suffix] [nchar](12) NOT NULL CONSTRAINT [DF_Invoices_Suffix] DEFAULT ('')
GO

DROP INDEX IX_gl_Invoices_For_Dupes ON [dbo].[gl_Invoices]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_gl_Invoices_For_Dupes] ON [dbo].[gl_Invoices]
(
	[VendorNumber] ASC,
	[Prefix] ASC,
	[InvoiceNumber] ASC,
	[Suffix] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].gl_InvoiceHistory 
ADD	[EFT] [bit] NULL,
	[Prefix] [nchar](6) NOT NULL CONSTRAINT [DF_InvoiceHistory_Prefix]  DEFAULT (''),
	[Suffix] [nchar](12) NOT NULL CONSTRAINT [DF_InvoiceHistory_Suffix] DEFAULT ('')
GO

INSERT INTO [dbo].[permission] (permission_type, permission_name, permission_description) VALUES(1, 'mbcs_reciplists', 'Email Recipient Lists') 
GO

/****** Object:  Table [dbo].[RecipientMaster]    Script Date: 2017-09-16 13:46:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RecipientMaster](
	[listId] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[listName] [nvarchar](32) NULL,
	[companyIds] [nvarchar](255) NULL,
	[buildingIds] [nvarchar](255) NULL,
	[tenantTypes] [nvarchar](255) NULL,
	[ownerFilter] [bit] NULL,
	[buildingFilter] [bit] NULL,
	[typeFilter] [bit] NULL,
	[arrearsFilter] [bit] NULL,
	[arrearsAmount] [money] NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [RecipientMaster$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[listId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RecipientMaster] ADD  DEFAULT ((0)) FOR [ownerFilter]
GO

ALTER TABLE [dbo].[RecipientMaster] ADD  DEFAULT ((0)) FOR [buildingFilter]
GO

ALTER TABLE [dbo].[RecipientMaster] ADD  DEFAULT ((0)) FOR [typeFilter]
GO

ALTER TABLE [dbo].[RecipientMaster] ADD  DEFAULT ((0)) FOR [arrearsFilter]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[listId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'listId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[accountId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'accountId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[listName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'listName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[companyIds]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'companyIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[buildingIds]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'buildingIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[tenantTypes]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'tenantTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[ownerFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'ownerFilter'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[buildingFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'buildingFilter'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[typeFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'typeFilter'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[arrearsFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'arrearsFilter'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[arrearsAmount]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'COLUMN',@level2name=N'arrearsAmount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[RecipientMaster].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecipientMaster', @level2type=N'CONSTRAINT',@level2name=N'RecipientMaster$PrimaryKey'
GO


/****** Object:  Table [dbo].[Recipients]    Script Date: 2017-09-16 13:47:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recipients](
	[recipient_Id] [int] IDENTITY(1,1) NOT NULL,
	[list_Id] [int] NULL,
	[first_name] [nvarchar](32) NULL,
	[last_name] [nvarchar](32) NULL,
	[email_address] [nvarchar](128) NULL,
	[address_one] [nvarchar](32) NULL,
	[address_two] [nvarchar](32) NULL,
	[address_three] [nvarchar](32) NULL,
	[city] [nvarchar](32) NULL,
	[state] [nvarchar](32) NULL,
	[zip] [nvarchar](10) NULL,
	[phone] [nvarchar](32) NULL,
	[date_of_birth] [datetime2](0) NULL,
 CONSTRAINT [Recipients$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[recipient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[recipient_Id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'recipient_Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[list_Id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'list_Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[first_name]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'first_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[last_name]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'last_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[email_address]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'email_address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[address_one]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'address_one'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[address_two]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'address_two'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[address_three]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'address_three'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[city]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'city'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[state]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'state'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[zip]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'zip'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[phone]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[date_of_birth]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'COLUMN',@level2name=N'date_of_birth'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[Recipients].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Recipients', @level2type=N'CONSTRAINT',@level2name=N'Recipients$PrimaryKey'
GO


