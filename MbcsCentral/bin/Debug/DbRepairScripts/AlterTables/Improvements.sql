ALTER TABLE [dbo].[pm_Improvements]
    ADD [RentIncreaseAmount] MONEY         DEFAULT 0 NOT NULL,
        [RentIncreaseDate]   DATETIME2 (7) NULL;
