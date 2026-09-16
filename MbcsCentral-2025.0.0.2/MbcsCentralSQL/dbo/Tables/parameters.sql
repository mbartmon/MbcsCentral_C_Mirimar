CREATE TABLE [dbo].[parameters] (
    [ID]            BIGINT         NOT NULL,
    [ClientId]      BIGINT         NOT NULL,
    [AcctId]        BIGINT         NOT NULL,
    [ParameterName] NVARCHAR (32)  NULL,
    [Values]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Parameters] PRIMARY KEY CLUSTERED ([ID] ASC)
);

