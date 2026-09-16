UPDATE DocumentLayoutFieldMaster SET Size = 12 WHERE FieldName IN ('NewLease', 'NewExp1', 'NewExp2')
GO
UPDATE DocumentLayoutFields SET Size = 12 WHERE FieldId IN (SELECT ID FROM DocumentLayoutFieldMaster WHERE FieldName IN ('NewLease', 'NewExp1', 'NewExp2'))
GO