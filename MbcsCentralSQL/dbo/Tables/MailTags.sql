CREATE TABLE [dbo].[MailTags] (
    [ID]      INT          IDENTITY (1, 1) NOT NULL,
    [MailTag] VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_MailTags] PRIMARY KEY CLUSTERED ([ID] ASC)
);

