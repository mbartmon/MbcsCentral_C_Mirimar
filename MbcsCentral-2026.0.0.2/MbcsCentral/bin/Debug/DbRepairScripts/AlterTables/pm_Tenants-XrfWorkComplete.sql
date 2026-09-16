ALTER TABLE [dbo].[pm_Tenants]
	ADD	[XrfWorkComplete]                      BIT           DEFAULT 0 NULL,
		[XrfWorkCompleteDate]                  DATETIME2 (7) DEFAULT '01/01/1900' NULL
GO