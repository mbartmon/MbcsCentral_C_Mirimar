CREATE TABLE [dbo].[Recipients] (
    [recipient_Id]  INT            IDENTITY (1, 1) NOT NULL,
    [list_Id]       INT            NULL,
    [first_name]    NVARCHAR (32)  NULL,
    [last_name]     NVARCHAR (32)  NULL,
    [email_address] NVARCHAR (128) NULL,
    [address_one]   NVARCHAR (32)  NULL,
    [address_two]   NVARCHAR (32)  NULL,
    [address_three] NVARCHAR (32)  NULL,
    [city]          NVARCHAR (32)  NULL,
    [state]         NVARCHAR (32)  NULL,
    [zip]           NVARCHAR (10)  NULL,
    [phone]         NVARCHAR (32)  NULL,
    [date_of_birth] DATETIME2 (0)  NULL,
    CONSTRAINT [Recipients$PrimaryKey] PRIMARY KEY CLUSTERED ([recipient_Id] ASC)
);

