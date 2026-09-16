CREATE TABLE [dbo].[NACHA_Layout]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Record_Type] NCHAR(1) NOT NULL DEFAULT '1', 
    [Position] INT NOT NULL DEFAULT 0, 
    [Length] INT NOT NULL DEFAULT 0, 
    [Requirement] NCHAR(1) NOT NULL DEFAULT 'O', 
    [Contents] VARCHAR(MAX) NOT NULL DEFAULT ' ', 
    [Name] VARCHAR(50) NOT NULL DEFAULT ' ', 
    [DataType] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [AK_NACHA_Layout_TypePosition] UNIQUE ([Record_Type] ASC, [Position] ASC)
)
