USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_HistChg]    Script Date: 4/15/2013 10:08:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_HistChg](
	[Building Number] [nvarchar](3) NULL,
	[Tenant Number] [nvarchar](4) NULL,
	[Sequence Number] [bigint] NULL,
	[Charge Number] [bigint] NULL,
	[Date] [datetime] NULL,
	[Code] [nvarchar](1) NULL,
	[Amount] [money] NULL,
	[Arrears] [money] NULL,
	[hist_chg_id] [bigint] IDENTITY(1,1) NOT NULL,
	[clientId] [bigint] NOT NULL,
 CONSTRAINT [PK_pm_HistChg] PRIMARY KEY CLUSTERED 
(
	[hist_chg_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[pm_HistChg] ADD  DEFAULT ((0)) FOR [Sequence Number]
GO

ALTER TABLE [dbo].[pm_HistChg] ADD  DEFAULT ((0)) FOR [Charge Number]
GO

ALTER TABLE [dbo].[pm_HistChg] ADD  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[pm_HistChg] ADD  DEFAULT ((0)) FOR [Arrears]
GO

ALTER TABLE [dbo].[pm_HistChg] ADD  DEFAULT ((0)) FOR [clientId]
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'915' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Building Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Building Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'780' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Tenant Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Tenant Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Tenant Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Sequence Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Sequence Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Sequence Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Charge Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Charge Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Charge Number'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'870' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Format', @value=N'Short Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Date'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'645' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'945' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Format', @value=N'$#,##0.00;($#,##0.00)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'1035' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'DefaultValue', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Format', @value=N'$#,##0.00;($#,##0.00)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Arrears' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Arrears' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg', @level2type=N'COLUMN',@level2name=N'Arrears'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'3/14/1998 7:09:38 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'12/21/2009 12:37:18 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderBy', @value=N'HistChg.[Building Number], HistChg.[Tenant Number]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'HistChg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'70884' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO

EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_HistChg'
GO


