INSERT INTO [dbo].[NACHA_Layout]
           ([Record_Type]
           ,[Position]
           ,[Length]
           ,[Requirement]
           ,[Contents]
           ,[Name]
           ,[DataType])
     VALUES
           (0, 21, 0, '', '', 'VendorBatchNumber', 0),
		   (0, 22, 0, '', '', 'VendorFileDirectory', 0),
		   (0, 23, 0, '', '', 'VendorFilePrefix', 0),
		   (0, 24, 0, '', '', 'VendorFileID', 0),
		   (0, 25, 0, '', '27', 'VendorOffsettingTransactionCode', 0),
		   (0, 26, 0, '', '22', 'VendorCheckingCredit', 0),
		   (0, 27, 0, '', '', 'VendorSavingsCredit', 0),
		   (0, 28, 0, '5', '', 'VendorEmailMessage', 0)

GO


