delete permission where permission_id not in
  (select min(permission_id)
  from   permission
  group  by permission_name)

IF EXISTS (SELECT name from sys.indexes  
           WHERE name = N'AK_Permission_Name')   
   DROP INDEX AK_Permission_Name ON Production.UnitMeasure;   
GO  
-- Create a unique index called AK_UnitMeasure_Name  
-- on the Production.UnitMeasure table using the Name column.  
CREATE UNIQUE INDEX AK_Permission_Name   
   ON permission (permission_name);   
GO  