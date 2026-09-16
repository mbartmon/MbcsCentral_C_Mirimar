USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[groups]    Script Date: 9/12/2013 10:30:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[groups](
	[group_id] [bigint] IDENTITY(1,1) NOT NULL,
	[group_name] [varchar](32) NOT NULL,
	[group_description] [varchar](128) NULL,
	[clientId] [bigint] NOT NULL,
 CONSTRAINT [PK_groups] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[groups] ADD  CONSTRAINT [DF_groups_clientId]  DEFAULT ((0)) FOR [clientId]
GO


