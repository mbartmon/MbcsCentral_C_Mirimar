INSERT INTO [dbo].[permission] (permission_type, permission_name, permission_description) VALUES(1, 'glchecknumbers', 'Manage Check Numbers') 
GO

ALTER TABLE [dbo].[gl_BatchJrnEntries] ADD AutoOffset bit NOT NULL DEFAULT 0
GO