CREATE TABLE [dbo].[user_permissions] (
    [permissions_id]  BIGINT IDENTITY (1, 1) NOT NULL,
    [user_id]         BIGINT NOT NULL,
    [permission_id]   INT    NOT NULL,
    [access]          BIT    NULL,
    [modify]          BIT    NULL,
    [remove]          BIT    NULL,
    [clientId]        BIGINT NOT NULL,
    [permission_type] INT    NOT NULL,
    [userType]        INT    NOT NULL,
    [print]           BIT    NULL,
    CONSTRAINT [PK_user_permissions] PRIMARY KEY CLUSTERED ([permissions_id] ASC)
);

