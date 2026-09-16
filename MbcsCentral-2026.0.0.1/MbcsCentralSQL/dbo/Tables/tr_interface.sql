CREATE TABLE [dbo].[tr_interface] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [prog_id] NCHAR (16)    NOT NULL,
    [app_id]  NCHAR (32)    NOT NULL,
    [desc]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_tm_interface] PRIMARY KEY CLUSTERED ([ID] ASC)
);

