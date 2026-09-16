USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_Payments]    Script Date: 4/15/2013 10:20:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_Payments](
	[Building Number] [nvarchar](3) NULL,
	[Tenant Number] [nvarchar](4) NULL,
	[Date of Entry] [datetime] NULL,
	[Amount] [money] NULL,
	[Security Code] [nvarchar](1) NULL,
	[Sequence Number] [bigint] NULL,
	[clientId] [bigint] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_pm_Payments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[pm_Payments] ADD  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[pm_Payments] ADD  DEFAULT ((0)) FOR [Sequence Number]
GO

ALTER TABLE [dbo].[pm_Payments] ADD  DEFAULT ((0)) FOR [clientId]
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Tenant Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Tenant Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Format', @value=N'Short Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Date of Entry' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Date of Entry' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Date of Entry'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Format', @value=N'$#,##0.00;($#,##0.00)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Security Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Security Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Security Code'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Sequence Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Sequence Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO

EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'3/14/1998 7:09:38 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO

EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'11/14/2010 6:10:53 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Payments' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO

EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'848' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO

EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Payments'
GO


