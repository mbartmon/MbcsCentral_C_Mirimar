CREATE TABLE [dbo].[DbControl] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [DbBackupLocation]  NVARCHAR (MAX) NOT NULL,
    [DbBackupPeriod]    INT            NOT NULL,
    [DbBackupLastDate]  DATE           NOT NULL,
    [DbBackupEnabled]   BIT            NOT NULL,
    [DbBackupTime]      TIME (0)       NOT NULL,
    [DbBackupClearDays] INT            NOT NULL,
    [DbManualLocation]  NVARCHAR (MAX) NOT NULL,
    [DbBackupStartDate] DATE           NOT NULL,
    [DbMappedDrive]     NCHAR (10)     NOT NULL,
    CONSTRAINT [PK_DbControl] PRIMARY KEY CLUSTERED ([ID] ASC)
);

