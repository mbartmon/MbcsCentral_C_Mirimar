CREATE TABLE [dbo].[Applications] (
    [applicationID]   INT          IDENTITY (1, 1) NOT NULL,
    [applicationName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([applicationID] ASC)
);

