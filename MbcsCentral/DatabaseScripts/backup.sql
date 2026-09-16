DECLARE @Path NVARCHAR(1000), @FileName NVARCHAR(255), @FullPath NVARCHAR(1255)
SET @Path = 'c:\MbcsCentralBackups\'
SET @FileName = 'MbcsCentralFishman_' + REPLACE(CONVERT(VARCHAR(10), GETDATE(), 101), '/', '') + '.bak'
SET @FullPath = @Path + @FileName

BACKUP DATABASE MbcsCentral_fishman TO DISK = @FullPath WITH INIT