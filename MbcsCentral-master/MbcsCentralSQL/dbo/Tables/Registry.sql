CREATE TABLE [dbo].[Registry] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [clientId]    BIGINT          NOT NULL,
    [acctId]      BIGINT          NOT NULL,
    [mainKey]     VARCHAR (50)    NOT NULL,
    [subKey]      VARCHAR (50)    NOT NULL,
    [value]       VARBINARY (MAX) NULL,
    [dataType]    INT             NOT NULL,
    [valuestring] VARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_RegistryNew] PRIMARY KEY CLUSTERED ([ID] ASC)
);

