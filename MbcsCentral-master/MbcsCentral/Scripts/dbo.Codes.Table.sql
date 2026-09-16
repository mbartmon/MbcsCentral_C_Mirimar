USE [MbcsCentral]
GO
/****** Object:  Table [dbo].[Codes]    Script Date: 11/15/2010 08:33:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Codes](
	[Charge Code] [nvarchar](1) NOT NULL,
	[Charge Description] [nvarchar](32) NULL,
	[GL Credit Account] [nvarchar](4) NULL,
	[GL Credit Sub-Account] [nvarchar](3) NULL,
	[GL Debit Account] [nvarchar](4) NULL,
	[GL Debit Sub-Account] [nvarchar](3) NULL,
	[GL Cash Account] [nvarchar](4) NULL,
	[Gl Cash Sub-Account] [nvarchar](3) NULL,
	[Legal Code] [bit] NULL,
 CONSTRAINT [aaaaaCodes_PK] PRIMARY KEY NONCLUSTERED 
(
	[Charge Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Charge Code] ON [dbo].[Codes] 
(
	[Charge Description] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Charge Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Charge Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Code'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'3444' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Charge Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'32' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Charge Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Charge Description'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'GL Credit Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'GL Credit Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'GL Credit Sub-Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'GL Credit Sub-Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Credit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'GL Debit Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'GL Debit Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'GL Debit Sub-Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'GL Debit Sub-Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Debit Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'GL Cash Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'GL Cash Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'GL Cash Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Gl Cash Sub-Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Gl Cash Sub-Account' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Gl Cash Sub-Account'
GO
EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Format', @value=N'Yes/No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Legal Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'9' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Legal Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes', @level2type=N'COLUMN',@level2name=N'Legal Code'
GO
EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'3/14/1998 7:09:37 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'12/21/2009 12:37:15 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Codes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'37' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Codes'
GO
ALTER TABLE [dbo].[Codes] ADD  DEFAULT ((0)) FOR [Legal Code]
GO
