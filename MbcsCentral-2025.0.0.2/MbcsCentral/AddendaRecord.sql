INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'7', 1, 1, N'M', N'7', N'RecordTypeCode', 1)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'7', 2, 2, N'M', N'05', N'TransactionCode', 1)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'7', 4, 80, N'O', N'', N'Data', 1)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'7', 84, 4, N'M', N'', N'AddendaSequenceNumber', 1)
GO
INSERT [dbo].[NACHA_Layout] ([Record_Type], [Position], [Length], [Requirement], [Contents], [Name], [DataType]) VALUES (N'7', 88, 7, N'M', N'', N'DetailSequenceNumber', 1)
GO


