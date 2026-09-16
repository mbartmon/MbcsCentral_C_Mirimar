CREATE TABLE [dbo].[GroupMembers] (
    [ID]        BIGINT IDENTITY (1, 1) NOT NULL,
    [groupId]   BIGINT NOT NULL,
    [accountId] BIGINT NOT NULL,
    CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

