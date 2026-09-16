DECLARE @BackupDirectory NVARCHAR(100) 
EXEC master..xp_instance_regread @rootkey = 'HKEY_LOCAL_MACHINE', @key = 'Software\\Microsoft\\MSSQLServer\\MSSQLServer', @value_name = 'BackupDirectory', @BackupDirectory = @BackupDirectory OUTPUT;  SELECT @BackupDirectory


BACKUP DATABASE MbcsCentral_fishman TO DISK =$(FileName) WITH INIT