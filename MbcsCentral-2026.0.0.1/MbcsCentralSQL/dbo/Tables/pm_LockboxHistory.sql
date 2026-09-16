CREATE TABLE [dbo].[pm_LockboxHistory]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Date] DATETIME NOT NULL DEFAULT '01/01/1900', 
    [FileName] NVARCHAR(MAX) NULL , 
    [Batches] NVARCHAR(MAX) NULL, 
    [TimeStamp] DATETIME NULL, 
    [User] NVARCHAR(50) NULL, 
    [Type] INT NOT NULL DEFAULT 0
)
