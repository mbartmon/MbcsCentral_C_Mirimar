USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_TenantsSnapShot]    Script Date: 4/16/2013 7:36:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[pm_TenantsSnapShot](
	[clientId] [bigint] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Sequence Number] [bigint] NOT NULL,
	[Building Number] [varchar](3) NOT NULL,
	[Tenant Number] [varchar](4) NOT NULL,
	[Tenant Name] [varchar](30) NOT NULL,
	[Apartment] [varchar](4) NOT NULL,
	[Rent] [money] NOT NULL,
	[Security] [money] NOT NULL,
	[Arrears] [money] NOT NULL,
	[Paid] [money] NOT NULL,
	[Last Paid Date] [datetime2](0) NOT NULL,
	[Total Charges] [money] NOT NULL,
	[RentLegal] [money] NOT NULL,
 CONSTRAINT [pm_TenantsSnapShot$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Apartment$disallow_zero_length] CHECK  ((len([Apartment])>(0)))
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot] CHECK CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Apartment$disallow_zero_length]
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Building Number$disallow_zero_length] CHECK  ((len([Building Number])>(0)))
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot] CHECK CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Building Number$disallow_zero_length]
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Tenant Name$disallow_zero_length] CHECK  ((len([Tenant Name])>(0)))
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot] CHECK CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Tenant Name$disallow_zero_length]
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Tenant Number$disallow_zero_length] CHECK  ((len([Tenant Number])>(0)))
GO

ALTER TABLE [dbo].[pm_TenantsSnapShot] CHECK CONSTRAINT [SSMA_CC$pm_TenantsSnapShot$Tenant Number$disallow_zero_length]
GO


