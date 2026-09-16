FOR /f %%a in ('WMIC OS GET LocalDateTime ^| find "."') DO Set _DTS=%%a
Set _datetime=%_DTS:~0,4%%_DTS:~4,2%%_DTS:~6,2%_%_DTS:~8,2%%_DTS:~10,2%%_DTS:~12,2%
Set _filename='\\NewServer\SqlBackup\MbcsCentral_fishman_%_datetime%.bak'
Set _source=\\NewServer\SqlBackup\MbcsCentral_fishman_%_datetime%.bak

sqlcmd -S NewServer\MBCSCENTRALSQL,1433 -U mbartmon -P Vietnam65 -i c:\users\fileroom\desktop\SqlDobackup.sql  -v FileName = %_filename%
copy %_source% /B %1:\MbcsCentralBackups\
del %_source%

@ECHO Database backed up from: %_source%