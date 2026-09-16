SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_LockboxHistory]
(   [Id]       [int] NOT NULL,
    [Date]     [datetime] NOT NULL DEFAULT '01/01/1900',
    [FileName] [nvarchar](MAX) NULL,
    [Batches]  [nvarchar](MAX) NULL,
    [TimeStamp] [datetime] NULL,
    [User] [nvarchar](50) NULL,
    CONSTRAINT [PK_pm_LockboxHistory] PRIMARY KEY CLUSTERED 
    (
	[Id] ASC
    )
)


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON


