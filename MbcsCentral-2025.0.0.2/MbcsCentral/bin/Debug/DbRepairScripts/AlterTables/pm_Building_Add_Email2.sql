ALTER TABLE pm_Building ADD Email2 VARCHAR(50) NOT NULL DEFAULT '';
GO
UPDATE  a SET a.Email2 = b.FaxNumber
FROM pm_Building a
INNER JOIN pm_Building b
ON a.[Building Number] =b.[Building Number];
GO