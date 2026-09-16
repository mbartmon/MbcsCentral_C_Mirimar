USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_GLTable]    Script Date: 4/15/2013 10:14:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_GLTable](
	[From] [nvarchar](3) NULL,
	[To] [nvarchar](3) NULL,
	[clientId] [bigint] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_pm_GLTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[pm_GLTable] ADD  DEFAULT ((0)) FOR [clientId]
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'From' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'GLTable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'From'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'To' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'To' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'GLTable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable', @level2type=N'COLUMN',@level2name=N'To'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO

EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'3/14/1998 7:09:38 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO

EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'3/14/1998 7:09:38 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'GLTable' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO

EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO

EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_GLTable'
GO


