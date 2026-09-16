CREATE TABLE [dbo].[groups] (
    [group_id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [group_name]        VARCHAR (32)  NOT NULL,
    [group_description] VARCHAR (128) NULL,
    [clientId]          BIGINT        NOT NULL,
    CONSTRAINT [PK_groups] PRIMARY KEY CLUSTERED ([group_id] ASC)
);

