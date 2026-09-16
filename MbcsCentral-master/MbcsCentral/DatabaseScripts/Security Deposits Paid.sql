IF OBJECT_ID('#Temp','U') IS NOT NULL
  DROP TABLE[#Temp];

CREATE TABLE [#Temp]
(
  Department smallint,
  CheckNumber int
);

INSERT INTO [#Temp]
	SELECT Department, CheckNumber FROM gl_ApChecks
	WHERE ID IN (select ID From gl_ApChecks where sequence = 1 AND [vendor/account] = 35);

SELECT c.CheckNumber, c.Date, c.Amount, FROM gl_ApChecks c
WHERE Sequence = 0 AND ((c.Department, c.CheckNumber) IN (SELECT t.Department, t.CheckNumber FROM [#Temp] t))

SELECT 