SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('#Temp', 'U') IS NOT NULL
  DROP TABLE #Temp; 

CREATE TABLE #Temp (
	[Record_Type] [nchar](2) NOT NULL,
	[Position] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[Requirement] [nchar](1) NOT NULL,
	[Contents] [varchar](max) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DataType] [int] NOT NULL)
GO

INSERT INTO #Temp SELECT Record_Type, Position, Length, Requirement, Contents, Name, DataType FROM [dbo].[NACHA_Layout]
GO

ALTER TABLE NACHA_Layout DROP CONSTRAINT IF EXISTS PK__tmp_ms_x__3214EC078275EB80
GO
ALTER TABLE NACHA_Layout DROP CONSTRAINT IF EXISTS AK_NACHA_Layout_TypePosition
GO

DROP TABLE NACHA_Layout
GO

CREATE TABLE [dbo].[NACHA_Layout] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Record_Type] [nchar](2) NOT NULL,
	[Position] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[Requirement] [nchar](1) NOT NULL,
	[Contents] [varchar](max) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DataType] [int] NOT NULL,
 CONSTRAINT [PK__tmp_ms_x__3214EC078275EB80] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_NACHA_Layout_TypePosition] UNIQUE NONCLUSTERED 
(
	[Record_Type] ASC,
	[Position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [dbo].NACHA_Layout
	SELECT Record_Type, Position, Length, Requirement, Contents, Name, DataType FROM #Temp
GO

DROP TABLE #Temp
GO