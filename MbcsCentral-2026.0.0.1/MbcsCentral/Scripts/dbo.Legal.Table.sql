USE [MbcsCentral]
GO

/****** Object:  Table [dbo].[pm_Legal]    Script Date: 4/15/2013 10:15:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pm_Legal](
	[ID] [bigint] NOT NULL,
	[Year] [smallint] NOT NULL,
	[AnWGChildren] [bit] NULL,
	[AnWGNoChildren] [bit] NULL,
	[AnWGWant] [bit] NULL,
	[AnWGInstalled] [bit] NULL,
	[AnWGNotInstalled] [bit] NULL,
	[AnWGMaintenance] [bit] NULL,
	[AnWGNoMaintenance] [bit] NULL,
	[RnWGChildren] [bit] NULL,
	[RnWGNoChildren] [bit] NULL,
	[RnWGWant] [bit] NULL,
	[AnLPChildren] [bit] NULL,
	[RnLPChildren] [bit] NULL,
	[AnLPChildrenNo] [bit] NULL,
	[RnLPChildrenNo] [bit] NULL,
	[legalId] [bigint] IDENTITY(1,1) NOT NULL,
	[clientId] [bigint] NOT NULL,
 CONSTRAINT [PK_pm_Legal] PRIMARY KEY CLUSTERED 
(
	[legalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGChild__32E0915F]  DEFAULT ((0)) FOR [AnWGChildren]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGNoChi__33D4B598]  DEFAULT ((0)) FOR [AnWGNoChildren]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGWant__34C8D9D1]  DEFAULT ((0)) FOR [AnWGWant]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGInsta__35BCFE0A]  DEFAULT ((0)) FOR [AnWGInstalled]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGNotIn__36B12243]  DEFAULT ((0)) FOR [AnWGNotInstalled]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGMaint__37A5467C]  DEFAULT ((0)) FOR [AnWGMaintenance]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnWGNoMai__38996AB5]  DEFAULT ((0)) FOR [AnWGNoMaintenance]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__RnWGChild__398D8EEE]  DEFAULT ((0)) FOR [RnWGChildren]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__RnWGNoChi__3A81B327]  DEFAULT ((0)) FOR [RnWGNoChildren]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__RnWGWant__3B75D760]  DEFAULT ((0)) FOR [RnWGWant]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnLPChild__3C69FB99]  DEFAULT ((0)) FOR [AnLPChildren]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__RnLPChild__3D5E1FD2]  DEFAULT ((0)) FOR [RnLPChildren]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__AnLPChild__3E52440B]  DEFAULT ((0)) FOR [AnLPChildrenNo]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  CONSTRAINT [DF__Legal__RnLPChild__3F466844]  DEFAULT ((0)) FOR [RnLPChildrenNo]
GO

ALTER TABLE [dbo].[pm_Legal] ADD  DEFAULT ((0)) FOR [clientId]
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DecimalPlaces', @value=N'255' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'109' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Year' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'Year' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'Year'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGNoChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGNoChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGWant' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGWant' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGInstalled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGInstalled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGNotInstalled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGNotInstalled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNotInstalled'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGMaintenance' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGMaintenance' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnWGNoMaintenance' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'9' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnWGNoMaintenance' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnWGNoMaintenance'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RnWGChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RnWGChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RnWGNoChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RnWGNoChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGNoChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RnWGWant' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RnWGWant' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnWGWant'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnLPChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnLPChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RnLPChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RnLPChildren' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildren'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'AnLPChildrenNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'AnLPChildrenNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'AnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'AllowZeroLength', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'AppendOnly', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'CollatingOrder', @value=N'1033' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnHidden', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnOrder', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'ColumnWidth', @value=N'-1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'DataUpdatable', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DisplayControl', @value=N'106' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'RnLPChildrenNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'OrdinalPosition', @value=N'16' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Required', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Size', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceField', @value=N'RnLPChildrenNo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'SourceTable', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Type', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal', @level2type=N'COLUMN',@level2name=N'RnLPChildrenNo'
GO

EXEC sys.sp_addextendedproperty @name=N'Attributes', @value=N'0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO

EXEC sys.sp_addextendedproperty @name=N'DateCreated', @value=N'5/8/2003 9:44:06 AM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO

EXEC sys.sp_addextendedproperty @name=N'LastUpdated', @value=N'11/14/2010 6:10:53 PM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=N'False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO

EXEC sys.sp_addextendedproperty @name=N'Name', @value=N'Legal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO

EXEC sys.sp_addextendedproperty @name=N'RecordCount', @value=N'3472' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO

EXEC sys.sp_addextendedproperty @name=N'Updatable', @value=N'True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pm_Legal'
GO


