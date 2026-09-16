USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_TenantsArchive]    Script Date: 4/15/2013 10:26:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[pm_TenantsArchive](
	[Building Number] [varchar](3) NOT NULL,
	[Tenant Number] [varchar](4) NOT NULL,
	[Sequence Number] [bigint] NOT NULL,
	[Old History File Pointer] [int] NULL,
	[New Tenant Flag] [bit] NULL,
	[Replacing Tenant] [bigint] NULL,
	[Tenant Name] [varchar](30) NULL,
	[Apartment] [varchar](4) NULL,
	[Due Date] [varchar](2) NULL,
	[Type] [varchar](2) NULL,
	[Alternate Billing Address Flag] [bit] NULL,
	[Additional Name Flag] [bit] NULL,
	[Lease Letter Flag] [bit] NULL,
	[Low Rent Flag] [bit] NULL,
	[Legal Code] [varchar](1) NULL,
	[Legal Code 2] [varchar](1) NULL,
	[Rent] [money] NULL,
	[Security] [money] NULL,
	[Arrears] [money] NULL,
	[Paid] [money] NULL,
	[Future Rent 1] [money] NULL,
	[Future Rent 2] [money] NULL,
	[Future Rent 3] [money] NULL,
	[MBR] [money] NULL,
	[Future Date 1] [datetime2](0) NULL,
	[Future Date 2] [datetime2](0) NULL,
	[Future Date 3] [datetime2](0) NULL,
	[Rooms] [real] NULL,
	[Square Feet] [real] NULL,
	[Move-in Date] [datetime2](0) NULL,
	[Lease Start Date] [datetime2](0) NULL,
	[Lease Expiration Date] [datetime2](0) NULL,
	[Last Paid Date] [datetime2](0) NULL,
	[Additional Name] [varchar](30) NULL,
	[SSNumber1] [varchar](11) NULL,
	[SSNumber2] [varchar](11) NULL,
	[HomePhone1] [varchar](12) NULL,
	[HomePhone2] [varchar](12) NULL,
	[BusPhone1] [varchar](21) NULL,
	[BusPhone2] [varchar](21) NULL,
	[AltBillName] [varchar](30) NULL,
	[AltBillAdd1] [varchar](30) NULL,
	[AltBillAdd2] [varchar](30) NULL,
	[AltBillAdd3] [varchar](30) NULL,
	[AddName] [varchar](30) NULL,
	[Bank] [varchar](60) NULL,
	[InterestInfo] [varchar](18) NULL,
	[Shares] [real] NULL,
	[Comment] [varchar](max) NULL,
	[Seniorcit] [varchar](18) NULL,
	[Total Charges] [money] NULL,
	[Pager] [varchar](12) NULL,
	[dhcrDateOne] [datetime2](0) NULL,
	[dhcrRentOne] [money] NULL,
	[dhcrStoveOne] [bit] NULL,
	[dhcrRefrigeratorOne] [bit] NULL,
	[dhcrDishwasherOne] [bit] NULL,
	[dhcrACOne] [bit] NULL,
	[dhcrWindowsOne] [bit] NULL,
	[dhcrOtherOne] [bit] NULL,
	[dhcrOtherTextOne] [varchar](16) NULL,
	[dhcrDateTwo] [datetime2](0) NULL,
	[dhcrRentTwo] [money] NULL,
	[dhcrStoveTwo] [bit] NULL,
	[dhcrRefrigeratorTwo] [bit] NULL,
	[dhcrDishwasherTwo] [bit] NULL,
	[dhcrACTwo] [bit] NULL,
	[dhcrWindowsTwo] [bit] NULL,
	[dhcrOtherTwo] [bit] NULL,
	[dhcrOtherTextTwo] [varchar](16) NULL,
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
	[Cell2] [varchar](12) NULL,
	[SecMultiple] [real] NULL,
	[Email1] [varchar](64) NULL,
	[Email2] [varchar](64) NULL,
	[Temp1] [varchar](64) NULL,
	[Temp2] [varchar](64) NULL,
	[SecurityAccount] [varchar](32) NULL,
	[OptTenant1] [smallint] NULL,
	[OptTenant2] [smallint] NULL,
	[SecurityBuilding] [varchar](32) NULL,
	[Succeeded] [bit] NULL,
	[SecondSuccession] [bit] NULL,
	[421a] [bit] NULL,
	[RentOvercharge] [bit] NULL,
	[FairMarketAppeal] [bit] NULL,
	[TransientOccupant] [bit] NULL,
	[OwnerOccupied] [bit] NULL,
	[CommercialProfessional] [bit] NULL,
	[NotPrimeResidence] [bit] NULL,
	[TempExemptOther] [bit] NULL,
	[ExemptDate] [datetime2](0) NULL,
	[ExemptHighRentVacancy] [bit] NULL,
	[ExemptLastLegalPaid] [money] NULL,
	[ExemptHighRentVacanyPeriod] [smallint] NULL,
	[ExemptHighRentIncome] [bit] NULL,
	[ExemptCommercialProfessionalCo] [bit] NULL,
	[ExemptSubstantialRehab] [bit] NULL,
	[ExemptCoopCondoOwner] [bit] NULL,
	[ExemptSec11243] [bit] NULL,
	[ExemptOther] [bit] NULL,
	[ExemptOtherDesc] [varchar](15) NULL,
	[ExemptExpSec608] [bit] NULL,
	[ExemptExp421a] [bit] NULL,
	[Exempt421aMarketRate] [bit] NULL,
	[Exempt421aIncome] [bit] NULL,
	[Exempt421aMedianIncome] [smallint] NULL,
	[clientId] [bigint] NOT NULL,
 CONSTRAINT [PK_TenantsArchive] PRIMARY KEY NONCLUSTERED 
(
	[Sequence Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [New Tenant Flag]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Alternate Billing Address Flag]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Additional Name Flag]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Lease Letter Flag]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Low Rent Flag]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrStoveOne]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrRefrigeratorOne]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrDishwasherOne]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrACOne]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrWindowsOne]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrOtherOne]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrStoveTwo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrRefrigeratorTwo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrDishwasherTwo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrACTwo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrWindowsTwo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [dhcrOtherTwo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [LegalFee]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [LegalFeeNo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [LateFeeYes]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [LateFeeNo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ('-') FOR [SecurityAccount]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Succeeded]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [SecondSuccession]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [421a]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [RentOvercharge]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [FairMarketAppeal]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [TransientOccupant]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [OwnerOccupied]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [CommercialProfessional]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [NotPrimeResidence]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [TempExemptOther]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptHighRentVacancy]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptHighRentIncome]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptCommercialProfessionalCo]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptSubstantialRehab]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptCoopCondoOwner]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptSec11243]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptOther]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptExpSec608]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [ExemptExp421a]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Exempt421aMarketRate]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  DEFAULT ((0)) FOR [Exempt421aIncome]
GO

ALTER TABLE [dbo].[pm_TenantsArchive] ADD  CONSTRAINT [DF_pm_TenantsArchive_clientId]  DEFAULT ((0)) FOR [clientId]
GO


