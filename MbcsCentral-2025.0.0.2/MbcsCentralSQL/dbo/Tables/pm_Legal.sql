CREATE TABLE [dbo].[pm_Legal] (
    [ID]                INT      NOT NULL,
    [Year]              SMALLINT NOT NULL,
    [AnWGChildren]      BIT      NULL,
    [AnWGNoChildren]    BIT      NULL,
    [AnWGWant]          BIT      NULL,
    [AnWGInstalled]     BIT      NULL,
    [AnWGNotInstalled]  BIT      NULL,
    [AnWGMaintenance]   BIT      NULL,
    [AnWGNoMaintenance] BIT      NULL,
    [RnWGChildren]      BIT      NULL,
    [RnWGNoChildren]    BIT      NULL,
    [RnWGWant]          BIT      NULL,
    [AnLPChildren]      BIT      NULL,
    [RnLPChildren]      BIT      NULL,
    [AnLPChildrenNo]    BIT      NULL,
    [RnLPChildrenNo]    BIT      NULL,
    [legalId]           INT      IDENTITY (1, 1) NOT NULL,
    [clientId]          INT      NOT NULL,
    CONSTRAINT [PK_pm_Legal] PRIMARY KEY CLUSTERED ([legalId] ASC)
);

