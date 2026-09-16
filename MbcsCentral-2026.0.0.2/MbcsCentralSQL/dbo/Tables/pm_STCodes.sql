CREATE TABLE [dbo].[pm_STCodes] (
    [Code]        NVARCHAR (1)  NOT NULL,
    [Key]         INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (32) NULL,
    [clientId]    INT           NOT NULL,
    CONSTRAINT [PK_pm_STCodes] PRIMARY KEY CLUSTERED ([Key] ASC)
);

