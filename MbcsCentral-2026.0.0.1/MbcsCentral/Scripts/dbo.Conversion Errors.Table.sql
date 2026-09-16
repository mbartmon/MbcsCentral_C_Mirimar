USE [MbcsCentral]
GO
/****** Object:  Table [dbo].[Conversion Errors]    Script Date: 11/15/2010 08:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversion Errors](
	[Object Type] [nvarchar](255) NULL,
	[Object Name] [nvarchar](255) NULL,
	[Error Description] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Object Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Object Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Conversion Errors' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Type'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Object Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Object Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Conversion Errors' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Object Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Error Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Error Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Conversion Errors' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors', @level2type=N'COLUMN',@level2name=N'Error Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors'
GO
EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'11/14/2010 5:44:41 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors'
GO
EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'11/14/2010 5:44:41 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Conversion Errors' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors'
GO
EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors'
GO
EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Conversion Errors'
GO
