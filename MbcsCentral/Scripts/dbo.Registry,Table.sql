USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[Registry]    Script Date: 9/28/2013 10:47:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Registry](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[clientId] [bigint] NOT NULL,
	[acctId] [bigint] NOT NULL,
	[mainKey] [varchar](50) NOT NULL,
	[subKey] [varchar](50) NOT NULL,
	[value] [varbinary](max) NOT NULL,
	[dataType] [int] NOT NULL,
 CONSTRAINT [PK_RegistryNew] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


