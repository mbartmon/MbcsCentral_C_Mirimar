USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 10/24/2013 9:11:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Accounts](
	[accountId] [bigint] IDENTITY(1,1) NOT NULL,
	[login] [varchar](32) NOT NULL,
	[accountType] [int] NOT NULL,
	[permissions] [int] NOT NULL,
	[password] [varchar](32) NOT NULL,
	[firstName] [varchar](32) NOT NULL,
	[lastName] [varchar](32) NOT NULL,
	[phone] [varchar](32) NULL,
	[mobilePhone] [varchar](32) NULL,
	[email] [varchar](50) NULL,
	[sms] [varchar](32) NULL,
	[ip] [varchar](32) NOT NULL,
	[port] [int] NOT NULL,
	[useOutlook] [bit] NOT NULL,
	[outlookFolder] [varchar](255) NULL,
	[parentAccount] [bigint] NOT NULL,
	[clientId] [bigint] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY NONCLUSTERED 
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO


