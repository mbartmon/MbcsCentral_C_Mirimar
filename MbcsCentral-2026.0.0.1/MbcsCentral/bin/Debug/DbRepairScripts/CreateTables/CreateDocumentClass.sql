/****** Object:  Table [dbo].[DocumentClasses]    Script Date: 5/24/2024 1:13:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DocumentClasses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL DEFAULT '',	
 CONSTRAINT [PK_DocumentClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocumentLayoutList] ADD DocumentClass int NULL;
GO

ALTER TABLE [dbo].[DocumentClasses] ADD  CONSTRAINT [DF_DocumentClasses_ClassName]  DEFAULT ('') FOR [ClassName]
GO

INSERT INTO [dbo].[DocumentClasses]
           ([ClassName], [Description])
     VALUES
           ('BankTenantDocumentsPrint', 'Bank Forms')
GO

