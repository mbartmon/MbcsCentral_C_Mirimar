USE [MbcsCentral]
GO
/****** Object:  Table [dbo].[PM_Tenants]    Script Date: 11/15/2010 08:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PM_Tenants](
	[Building Number] [nvarchar](3) NOT NULL,
	[Tenant Number] [nvarchar](4) NOT NULL,
	[Sequence Number] [int] IDENTITY(1,1) NOT NULL,
	[Old History File Pointer] [int] NULL,
	[New Tenant Flag] [bit] NULL,
	[Replacing Tenant] [int] NULL,
	[Tenant Name] [nvarchar](30) NULL,
	[Apartment] [nvarchar](4) NULL,
	[Due Date] [nvarchar](2) NULL,
	[Type] [nvarchar](2) NULL,
	[Alternate Billing Address Flag] [bit] NULL,
	[Additional Name Flag] [bit] NULL,
	[Lease Letter Flag] [bit] NULL,
	[Low Rent Flag] [bit] NULL,
	[Legal Code] [nvarchar](1) NULL,
	[Legal Code 2] [nvarchar](1) NULL,
	[Rent] [money] NULL,
	[Security] [money] NULL,
	[Arrears] [money] NULL,
	[Paid] [money] NULL,
	[Future Rent 1] [money] NULL,
	[Future Rent 2] [money] NULL,
	[Future Rent 3] [money] NULL,
	[MBR] [money] NULL,
	[Future Date 1] [datetime] NULL,
	[Future Date 2] [datetime] NULL,
	[Future Date 3] [datetime] NULL,
	[Rooms] [real] NULL,
	[Square Feet] [real] NULL,
	[Move-in Date] [datetime] NULL,
	[Lease Start Date] [datetime] NULL,
	[Lease Expiration Date] [datetime] NULL,
	[Last Paid Date] [datetime] NULL,
	[Additional Name] [nvarchar](30) NULL,
	[SSNumber1] [nvarchar](11) NULL,
	[SSNumber2] [nvarchar](11) NULL,
	[HomePhone1] [nvarchar](12) NULL,
	[HomePhone2] [nvarchar](12) NULL,
	[BusPhone1] [nvarchar](21) NULL,
	[BusPhone2] [nvarchar](21) NULL,
	[AltBillName] [nvarchar](30) NULL,
	[AltBillAdd1] [nvarchar](30) NULL,
	[AltBillAdd2] [nvarchar](30) NULL,
	[AltBillAdd3] [nvarchar](30) NULL,
	[AddName] [nvarchar](30) NULL,
	[Bank] [nvarchar](60) NULL,
	[InterestInfo] [nvarchar](18) NULL,
	[Shares] [real] NULL,
	[Comment] [ntext] NULL,
	[Seniorcit] [nvarchar](18) NULL,
	[Total Charges] [money] NULL,
	[Pager] [nvarchar](12) NULL,
	[dhcrDateOne] [datetime] NULL,
	[dhcrRentOne] [money] NULL,
	[dhcrStoveOne] [bit] NULL,
	[dhcrRefrigeratorOne] [bit] NULL,
	[dhcrDishwasherOne] [bit] NULL,
	[dhcrACOne] [bit] NULL,
	[dhcrWindowsOne] [bit] NULL,
	[dhcrOtherOne] [bit] NULL,
	[dhcrOtherTextOne] [nvarchar](16) NULL,
	[dhcrDateTwo] [datetime] NULL,
	[dhcrRentTwo] [money] NULL,
	[dhcrStoveTwo] [bit] NULL,
	[dhcrRefrigeratorTwo] [bit] NULL,
	[dhcrDishwasherTwo] [bit] NULL,
	[dhcrACTwo] [bit] NULL,
	[dhcrWindowsTwo] [bit] NULL,
	[dhcrOtherTwo] [bit] NULL,
	[dhcrOtherTextTwo] [nvarchar](16) NULL,
	[LegalFee] [bit] NULL,
	[LateFee] [money] NULL,
	[LateBase] [money] NULL,
	[LegalFeeNo] [bit] NULL,
	[LateFeeYes] [bit] NULL,
	[LateFeeNo] [bit] NULL,
	[RentLegal] [money] NULL,
	[Future Legal 1] [money] NULL,
	[Future Legal 2] [money] NULL,
	[Future Legal 3] [money] NULL,
	[ProposedRent] [money] NULL,
	[PromptPay1] [money] NULL,
	[ProposedRent2] [money] NULL,
	[PromptPay2] [money] NULL,
	[ProposedLegal2] [money] NULL,
	[ProposedLegal] [money] NULL,
	[Cell2] [nvarchar](12) NULL,
	[SecMultiple] [real] NULL,
	[Email1] [nvarchar](64) NULL,
	[Email2] [nvarchar](64) NULL,
	[Temp1] [nvarchar](64) NULL,
	[Temp2] [nvarchar](64) NULL,
	[SecurityAccount] [nvarchar](32) NULL,
	[OptTenant1] [smallint] NULL,
	[OptTenant2] [smallint] NULL,
	[SecurityBuilding] [nvarchar](32) NULL,
	[upsize_ts] [timestamp] NULL,
 CONSTRAINT [aaaaaTenants_PK] PRIMARY KEY NONCLUSTERED 
(
	[Building Number] ASC,
	[Tenant Number] ASC,
	[Sequence Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [{4540E5A5-BB6F-11D1-817D-841208C10000}] ON [dbo].[PM_Tenants] 
(
	[Building Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [BuildingTenants] ON [dbo].[PM_Tenants] 
(
	[Building Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [HistoryKey] ON [dbo].[PM_Tenants] 
(
	[Building Number] ASC,
	[Tenant Number] ASC,
	[Sequence Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Legal Code] ON [dbo].[PM_Tenants] 
(
	[Legal Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Paid] ON [dbo].[PM_Tenants] 
(
	[Paid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [SequenceNumber] ON [dbo].[PM_Tenants] 
(
	[Sequence Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [TenantName] ON [dbo].[PM_Tenants] 
(
	[Tenant Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [TenantNumber] ON [dbo].[PM_Tenants] 
(
	[Building Number] ASC,
	[Tenant Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Tenant Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Tenant Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Sequence Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Sequence Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Old History File Pointer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Old History File Pointer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Old History File Pointer'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'New Tenant Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'New Tenant Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'New Tenant Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Replacing Tenant' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Replacing Tenant' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Replacing Tenant'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Tenant Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Tenant Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Tenant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Apartment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Apartment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Due Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'9' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Due Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Due Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Alternate Billing Address Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Alternate Billing Address Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Alternate Billing Address Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Additional Name Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Additional Name Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Lease Letter Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Lease Letter Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Letter Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Low Rent Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Low Rent Flag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Low Rent Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Legal Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Legal Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Legal Code 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'16' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Legal Code 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Legal Code 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Rent' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Rent' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rent'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Security' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Security' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Security'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Arrears' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Arrears' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Arrears'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Paid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Paid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Paid'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Rent 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Rent 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Rent 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Rent 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Rent 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Rent 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Rent 3'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'MBR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'MBR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'MBR'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Date 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Date 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Date 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Date 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Date 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Date 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Date 3'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Rooms' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Rooms' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Rooms'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Square Feet' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Square Feet' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Square Feet'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Move-in Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Move-in Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Move-in Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Lease Start Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'31' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Lease Start Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Start Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Lease Expiration Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'32' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Lease Expiration Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Lease Expiration Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Last Paid Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'33' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Last Paid Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Last Paid Date'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Additional Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'34' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Additional Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Additional Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'SSNumber1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'35' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'SSNumber1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'SSNumber2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'36' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'SSNumber2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SSNumber2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'HomePhone1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'37' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'HomePhone1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'HomePhone2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'38' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'HomePhone2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'HomePhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'BusPhone1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'39' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'BusPhone1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'BusPhone2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'40' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'BusPhone2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'BusPhone2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AltBillName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'41' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AltBillName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillName'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AltBillAdd1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'42' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AltBillAdd1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AltBillAdd2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'43' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AltBillAdd2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AltBillAdd3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'44' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AltBillAdd3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AltBillAdd3'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AddName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'45' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AddName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'AddName'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Bank' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'46' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'60' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Bank' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Bank'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'InterestInfo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'47' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'InterestInfo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'InterestInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Shares' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'48' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Shares' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Comment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'49' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Comment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Seniorcit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'50' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Seniorcit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Seniorcit'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Total Charges' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'51' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Total Charges' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Total Charges'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Pager' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'52' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Pager' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Pager'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrDateOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'53' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrDateOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrRentOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'54' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrRentOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrStoveOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'55' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrStoveOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrRefrigeratorOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'56' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrRefrigeratorOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrDishwasherOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'57' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrDishwasherOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrACOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'58' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrACOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrWindowsOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'59' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrWindowsOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrOtherOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'60' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrOtherOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrOtherTextOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'61' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'16' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrOtherTextOne' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextOne'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrDateTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'62' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrDateTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDateTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrRentTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'63' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrRentTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRentTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrStoveTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'64' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrStoveTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrStoveTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrRefrigeratorTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'65' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrRefrigeratorTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrRefrigeratorTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrDishwasherTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'66' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrDishwasherTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrDishwasherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrACTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'67' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrACTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrACTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrWindowsTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'68' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrWindowsTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrWindowsTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrOtherTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'69' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrOtherTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'dhcrOtherTextTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'70' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'16' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'dhcrOtherTextTwo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'dhcrOtherTextTwo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'LegalFee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'71' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'LegalFee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFee'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'LateFee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'72' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'LateFee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFee'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'LateBase' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'73' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'LateBase' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateBase'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'LegalFeeNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'74' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'LegalFeeNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LegalFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'LateFeeYes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'75' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'LateFeeYes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeYes'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'LateFeeNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'76' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'LateFeeNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'LateFeeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RentLegal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'77' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RentLegal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'RentLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Legal 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'78' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Legal 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Legal 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'79' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Legal 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Future Legal 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'80' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Future Legal 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Future Legal 3'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'ProposedRent' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'81' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'ProposedRent' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'PromptPay1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'82' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'PromptPay1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'ProposedRent2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'83' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'ProposedRent2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedRent2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'PromptPay2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'84' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'PromptPay2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'PromptPay2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'ProposedLegal2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'85' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'ProposedLegal2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'ProposedLegal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'86' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'ProposedLegal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'ProposedLegal'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Cell2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'87' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Cell2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Cell2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'SecMultiple' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'88' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'SecMultiple' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecMultiple'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Email1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'89' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'64' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Email1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Email2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'90' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'64' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Email2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Email2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Temp1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'91' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'64' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Temp1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Temp2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'92' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'64' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Temp2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'Temp2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'SecurityAccount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'93' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'32' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'SecurityAccount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'OptTenant1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'94' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'OptTenant1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'OptTenant2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'95' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'OptTenant2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'OptTenant2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'SecurityBuilding' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'96' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'32' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'SecurityBuilding' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants', @level2type=N'COLUMN',@level2name=N'SecurityBuilding'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants'
GO
EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'3/14/1998 7:09:39 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants'
GO
EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'11/14/2010 6:10:53 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Tenants' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants'
GO
EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'851' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants'
GO
EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tenants'
GO
ALTER TABLE [dbo].[PM_Tenants]  WITH CHECK ADD  CONSTRAINT [Tenants_FK00] FOREIGN KEY([Building Number])
REFERENCES [dbo].[PM_Building] ([Building Number])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PM_Tenants] CHECK CONSTRAINT [Tenants_FK00]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [New Tenant Flag]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [Alternate Billing Address Flag]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [Additional Name Flag]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [Lease Letter Flag]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [Low Rent Flag]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrStoveOne]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrRefrigeratorOne]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrDishwasherOne]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrACOne]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrWindowsOne]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrOtherOne]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrStoveTwo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrRefrigeratorTwo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrDishwasherTwo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrACTwo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrWindowsTwo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [dhcrOtherTwo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [LegalFee]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [LegalFeeNo]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [LateFeeYes]
GO
ALTER TABLE [dbo].[PM_Tenants] ADD  DEFAULT ((0)) FOR [LateFeeNo]
GO
