CREATE TABLE [dbo].[permission] (
    [permission_id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [permission_type]        INT           NOT NULL,
    [permission_name]        VARCHAR (50)  NOT NULL,
    [permission_description] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_permission] PRIMARY KEY CLUSTERED ([permission_id] ASC)
);

