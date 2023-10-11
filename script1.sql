﻿CREATE TABLE USERREGISTER(
		IDUSER				UNIQUEIDENTIFIER	NOT NULL,
		NAME				NVARCHAR(150)		NOT NULL,
		EMAIL				NVARCHAR(100)		NOT NULL UNIQUE,
		PASSWORD			NVARCHAR(100)		NOT NULL,
		DATETIMECREATION	DATETIME			NOT NULL,

		PRIMARY KEY(IDUSER)

)
GO

DROP TABLE USERREGISTER

SELECT * FROM USERREGISTER