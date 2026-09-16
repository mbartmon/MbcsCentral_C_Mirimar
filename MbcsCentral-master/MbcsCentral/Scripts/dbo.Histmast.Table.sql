USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_Histmast]    Script Date: 4/15/2013 10:11:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_Histmast](
	[Building Number] [nvarchar](3) NOT NULL,
	[Tenant Number] [nvarchar](4) NOT NULL,
	[Sequence Number] [bigint] NOT NULL,
	[Tenant Name] [nvarchar](30) NULL,
	[Apartment] [nvarchar](4) NULL,
	[clientId] [bigint] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_pm_Histmast] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[pm_Histmast] ADD  DEFAULT ((0)) FOR [Sequence Number]
GO

ALTER TABLE [dbo].[pm_Histmast] ADD  DEFAULT ((0)) FOR [clientId]
GO


