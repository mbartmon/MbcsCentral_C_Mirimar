CREATE TABLE [dbo].[group_permissions] (
    [permissions_id] BIGINT IDENTITY (1, 1) NOT NULL,
    [group_id]       BIGINT NOT NULL,
    [permission_id]  BIGINT NOT NULL,
    [access]         BIT    NULL,
    [modify]         BIT    NULL,
    [remove]         BIT    NULL,
    [clientId]       INT    NOT NULL,
    CONSTRAINT [PK_group_permissions] PRIMARY KEY CLUSTERED ([permissions_id] ASC)
);

