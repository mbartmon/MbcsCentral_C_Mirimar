INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 1, 1, N'M', N'5', N'RecordTypeCode', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 2, 3, N'M', N'200', N'ServiceClassCode', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 5, 16, N'M', N'', N'CompanyName', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 21, 20, N'O', N'', N'CompanyDiscretionaryData', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 41, 10, N'M', N'', N'CompanyIdentification', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 51, 3, N'M', N'PPD', N'StandardEntryClassCode', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 54, 10, N'M', N'Payments', N'CompanyEntryDescription', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 64, 6, N'O', N'', N'CompanyDescriptiveDate', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 70, 6, N'M', N'yyMMdd', N'EffectiveDate', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 76, 3, N'O', N'', N'SettlementDate', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 79, 1, N'M', N'1', N'OriginatorStatusCode', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 80, 8, N'M', N'02120138', N'OriginatingDfiIdentification', 0)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'20', 88, 7, N'M', N'', N'BatchNumber', 0)
GO
