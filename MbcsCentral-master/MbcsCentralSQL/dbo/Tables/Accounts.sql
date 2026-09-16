CREATE TABLE [dbo].[Accounts] (
    [accountId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [login]         VARCHAR (32)  NOT NULL,
    [accountType]   INT           NOT NULL,
    [permissions]   INT           NOT NULL,
    [password]      VARCHAR (32)  NOT NULL,
    [firstName]     VARCHAR (32)  NOT NULL,
    [lastName]      VARCHAR (32)  NOT NULL,
    [phone]         VARCHAR (32)  NULL,
    [mobilePhone]   VARCHAR (32)  NULL,
    [email]         VARCHAR (50)  NULL,
    [sms]           VARCHAR (32)  NULL,
    [ip]            VARCHAR (32)  NOT NULL,
    [port]          INT           NOT NULL,
    [useOutlook]    BIT           NOT NULL,
    [outlookFolder] VARCHAR (255) NULL,
    [parentAccount] INT           NOT NULL,
    [clientId]      BIGINT        NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([accountId] ASC)
);

