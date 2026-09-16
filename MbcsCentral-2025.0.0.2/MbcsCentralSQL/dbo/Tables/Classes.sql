CREATE TABLE [dbo].[Classes] (
    [ID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [clientId] INT           NOT NULL,
    [acctId]   BIGINT        NOT NULL,
    [name]     VARCHAR (50)  NOT NULL,
    [value]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

