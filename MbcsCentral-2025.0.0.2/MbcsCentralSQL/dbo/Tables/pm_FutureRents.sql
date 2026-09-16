CREATE TABLE [dbo].[pm_FutureRents] (
    [ID]             INT   IDENTITY (1, 1) NOT NULL,
    [TenantSequence] INT   NOT NULL,
    [FutureDate]     DATE  NULL,
    [FutureAmount]   MONEY NOT NULL,
    [FutureLegal]    MONEY NOT NULL,
    CONSTRAINT [PK_pm_FutureRents] PRIMARY KEY CLUSTERED ([ID] ASC)
);

