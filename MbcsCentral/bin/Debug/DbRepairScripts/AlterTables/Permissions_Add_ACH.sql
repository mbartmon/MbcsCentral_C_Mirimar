INSERT INTO [dbo].[permission]
           ([permission_type]
           ,[permission_name]
           ,[permission_description])
     VALUES
           (1, 'gl_achreport', 'Print ACH Report')
GO

INSERT INTO [dbo].[permission]
           ([permission_type]
           ,[permission_name]
           ,[permission_description])
     VALUES
           (1, 'gl_achpayment', 'Generate ACH file')
GO

INSERT INTO [dbo].[permission]
           ([permission_type]
           ,[permission_name]
           ,[permission_description])
     VALUES
           (1, 'gl_postach', 'Post ACH invoices')
GO

INSERT INTO [dbo].[permission]
           ([permission_type]
           ,[permission_name]
           ,[permission_description])
     VALUES
           (1, 'gl_achaggreport', 'Print Aggregated ACH Report')
GO
