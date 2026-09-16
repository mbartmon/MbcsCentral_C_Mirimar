ALTER TABLE [dbo].[pm_Tenants]
	ADD	[EmployeeOccupied]               BIT           DEFAULT 0 NULL,
		[XrfTested]                      BIT           DEFAULT 0 NULL,
		[XrfExempt]                      BIT           DEFAULT 0 NULL,
		[XrfTestedDate]                  DATETIME2 (7) DEFAULT '01/01/1900' NULL,
		[XrfExemptDate]                  DATETIME2 (7) DEFAULT '01/01/1900' NULL;
GO