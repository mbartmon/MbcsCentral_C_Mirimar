USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_Control]    Script Date: 7/29/2013 1:23:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_Control](
	[Status Report Grouping] [nvarchar](1) NULL,
	[Closing Date] [datetime] NULL,
	[Billing date] [datetime] NULL,
	[Last Reorg Date] [datetime] NULL,
	[Last Roll Date] [datetime] NULL,
	[Security Tracker] [bit] NOT NULL,
	[General Ledger] [bit] NOT NULL,
	[Return Address Flag] [smallint] NULL,
	[Return Address A1] [nvarchar](30) NULL,
	[Return Address A2] [nvarchar](30) NULL,
	[Return Address A3] [nvarchar](30) NULL,
	[Return Address Bname] [nvarchar](30) NULL,
	[Return Address B1] [nvarchar](30) NULL,
	[Return Address B2] [nvarchar](30) NULL,
	[Return Address B3] [nvarchar](30) NULL,
	[Pay To Flag] [smallint] NULL,
	[Pay To Name] [nvarchar](30) NULL,
	[Statement Message 1] [nvarchar](60) NULL,
	[Statement Message 2] [nvarchar](60) NULL,
	[Statement Message 3] [nvarchar](60) NULL,
	[General Msg Location] [smallint] NULL,
	[Legal Msg Location] [smallint] NULL,
	[Type Msg Location] [smallint] NULL,
	[Telephone Flag] [smallint] NULL,
	[Telephone Number] [nvarchar](12) NULL,
	[Separate Charge Codes] [nvarchar](30) NULL,
	[Low Rent Cut-Off] [money] NULL,
	[Low Rent Allowance] [money] NULL,
	[Low Rent Max 1] [money] NULL,
	[Low Rent Max 2] [money] NULL,
	[CleanFlag] [bit] NOT NULL,
	[CleanDays] [smallint] NULL,
	[ErrorLogFlag] [bit] NOT NULL,
	[ErrorLogDays] [smallint] NULL,
	[HistoryFlag] [bit] NOT NULL,
	[HistoryYears] [smallint] NULL,
	[MaintFlag] [bit] NOT NULL,
	[MaintYears] [smallint] NULL,
	[CleanDate] [datetime] NULL,
	[Percent One] [real] NULL,
	[Percent Two] [real] NULL,
	[TypeColor] [int] NULL,
	[LegalColor] [int] NULL,
	[GeneralColor] [int] NULL,
	[MinimumIncrease1] [money] NULL,
	[MinimumIncrease2] [money] NULL,
	[MinimumIncreaseRent] [money] NULL,
	[MinimumIncreaseYears] [smallint] NULL,
	[duplexFile] [nvarchar](128) NULL,
	[duplexEnabled] [bit] NOT NULL,
	[alertMessages] [bit] NOT NULL,
	[scan] [bit] NOT NULL,
	[Senior Citizen Code] [nvarchar](1) NULL,
	[clientId] [bigint] NOT NULL,
 CONSTRAINT [PK_pm_Control] PRIMARY KEY CLUSTERED 
(
	[clientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[pm_Control] ADD  DEFAULT ((0)) FOR [clientId]
GO


