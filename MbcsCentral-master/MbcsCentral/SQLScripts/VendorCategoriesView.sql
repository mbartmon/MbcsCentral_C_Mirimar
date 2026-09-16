USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[gl_Department]    Script Date: 2/13/2014 7:39:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[gl_Department](
	[Department Number] [smallint] NULL,
	[Name] [nvarchar](50) NULL,
	[Street Address] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](2) NULL,
	[Zip] [nvarchar](11) NULL,
	[Company Number] [smallint] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [Department$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[gl_Department] ADD  DEFAULT ((0)) FOR [Department Number]
GO

ALTER TABLE [dbo].[gl_Department] ADD  DEFAULT ((0)) FOR [Company Number]
GO


