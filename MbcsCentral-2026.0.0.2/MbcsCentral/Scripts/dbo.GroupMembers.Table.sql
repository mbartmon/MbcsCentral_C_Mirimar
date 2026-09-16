USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[GroupMembers]    Script Date: 9/13/2013 11:35:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GroupMembers](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[groupId] [bigint] NOT NULL,
	[accountId] [bigint] NOT NULL
) ON [PRIMARY]

GO

