USE [MbcsCentral]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 11/15/2010 08:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[Building Number] [nvarchar](3) NOT NULL,
	[Owner Number] [smallint] NULL,
	[Address 1] [nvarchar](30) NULL,
	[Address 2] [nvarchar](30) NULL,
	[Agent Name] [nvarchar](12) NULL,
	[Block Number] [nvarchar](4) NULL,
	[Lot Number] [nvarchar](3) NULL,
	[Section Number] [nvarchar](3) NULL,
	[Multiple Dwelling Registration Number] [nvarchar](6) NULL,
	[RSA] [nvarchar](6) NULL,
	[Senior Citizen Number] [nvarchar](6) NULL,
	[Superintendant Name] [nvarchar](18) NULL,
	[Superintendant Phone] [nvarchar](12) NULL,
	[Real Estate Taxes] [float] NULL,
	[Mortgage Interest] [float] NULL,
	[Total Shares] [int] NULL,
	[upsize_ts] [timestamp] NULL,
 CONSTRAINT [aaaaaBuilding_PK] PRIMARY KEY NONCLUSTERED 
(
	[Building Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [BuildingOwner Number] ON [dbo].[Building] 
(
	[Owner Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'585' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Building Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'1245' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Owner Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Owner Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Owner Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'2730' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Address 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Address 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 1'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'2385' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Address 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Address 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Address 2'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Agent Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Agent Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Agent Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'660' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Block Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Block Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Block Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'465' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Lot Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Lot Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Lot Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'510' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Section Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Section Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Section Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'930' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Multiple Dwelling Registration Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Multiple Dwelling Registration Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Multiple Dwelling Registration Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'660' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RSA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'9' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RSA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'RSA'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'990' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Senior Citizen Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Senior Citizen Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Senior Citizen Number'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'2325' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Superintendant Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Superintendant Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Name'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Superintendant Phone' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Superintendant Phone' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Superintendant Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Real Estate Taxes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Real Estate Taxes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Real Estate Taxes'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Mortgage Interest' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Mortgage Interest' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Mortgage Interest'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Total Shares' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Total Shares' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building', @level2type=N'COLUMN',@level2name=N'Total Shares'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'3/14/1998 7:09:36 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'11/14/2010 6:10:53 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Building' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Building'
GO
ALTER TABLE [dbo].[Building]  WITH CHECK ADD  CONSTRAINT [Building_FK00] FOREIGN KEY([Owner Number])
REFERENCES [dbo].[Owner] ([Owner Number])
GO
ALTER TABLE [dbo].[Building] CHECK CONSTRAINT [Building_FK00]
GO
ALTER TABLE [dbo].[Building] ADD  DEFAULT ((0)) FOR [Owner Number]
GO
ALTER TABLE [dbo].[Building] ADD  DEFAULT ((0)) FOR [Real Estate Taxes]
GO
ALTER TABLE [dbo].[Building] ADD  DEFAULT ((0)) FOR [Mortgage Interest]
GO
ALTER TABLE [dbo].[Building] ADD  DEFAULT ((0)) FOR [Total Shares]
GO
