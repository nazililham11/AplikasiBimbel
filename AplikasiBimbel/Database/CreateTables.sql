CREATE DATABASE DatabaseBimbel
GO
USE DatabaseBimbel
GO

CREATE TABLE Students (
  Student_ID	INT 		NOT NULL PRIMARY KEY IDENTITY(1,1),
  Name 			VARCHAR(40) NOT NULL,
  Nickname 		VARCHAR(30) NOT NULL,
  Password 		VARCHAR(30) NULL DEFAULT NULL,
  School 		VARCHAR(40) NOT NULL,
  Grade 		VARCHAR(20) NOT NULL,
  Status 		VARCHAR(20) NOT NULL DEFAULT 'Aktif' CHECK (Status IN('Aktif', 'Tidak Aktif')),
  IsPasswordEnabled BIT		NOT NULL DEFAULT '0',
  DateAdded		DATETIME	NOT NULL,
  DateNotActive DATETIME	NULL,
)
GO
CREATE TABLE Teachers (
  Teacher_ID	INT		 	 NOT NULL PRIMARY KEY,
  Username 		VARCHAR(30)  NOT NULL,
  Password 		VARCHAR(30)  NULL,
  Name 			VARCHAR(50)  NOT NULL,
  Address 		VARCHAR(100) NOT NULL,
  PhoneNumber	VARCHAR(15)  NOT NULL,
  Permission 	VARCHAR(20)	 NOT NULL DEFAULT 'Guru' CHECK (Permission IN('Super Admin', 'Admin', 'Guru')),
  Status		VARCHAR(20)  NOT NULL DEFAULT 'Aktif' CHECK(Status IN('Aktif', 'Tidak Aktif')),
  DateIn		DATETIME	 NOT NULL DEFAULT GETDATE(),
  DateOut		DATETIME	 NULL DEFAULT NULL
)
DROP TABLE Teachers
GO
CREATE TABLE Programs (
  Program_ID 	INT		 		NOT NULL PRIMARY KEY IDENTITY(1,1),
  Name 			VARCHAR(30)		NOT NULL
)
GO
CREATE TABLE StudentPrograms (
	Student_ID INT NOT NULL,
	Program_ID INT NOT NULL,
	FOREIGN KEY (Student_ID) REFERENCES Students(Student_ID) ON DELETE CASCADE,
	FOREIGN KEY (Program_ID) REFERENCES Programs(Program_ID) ON DELETE CASCADE
)
GO
CREATE TABLE Packages (
  Package_ID 	INT		 	NOT NULL PRIMARY KEY IDENTITY(1,1),
  Package 		VARCHAR(30) NOT NULL,
  Course 		VARCHAR(30) NOT NULL,
  Level 		VARCHAR(30) NOT NULL,
  Description	TEXT 		NOT NULL,
  Time 			INT		 	NOT NULL,
  MinimumScore	FLOAT 		NOT NULL
)
GO

CREATE TABLE Reports (
  Report_ID 		INT		 	NOT NULL PRIMARY KEY IDENTITY(1,1),
  Student_ID 		INT		 	NOT NULL,
  Package_ID 		INT		 	NOT NULL,
  StartTime 		DATETIME 	NOT NULL,
  FinishTime 		DATETIME 	NOT NULL,
  Report_Status 	VARCHAR(20) NOT NULL,
  Score 			FLOAT 		NOT NULL,
  FOREIGN KEY (Student_ID) REFERENCES Students(Student_ID) ON DELETE CASCADE,
  FOREIGN KEY (Package_ID) REFERENCES Packages(Package_ID) ON DELETE CASCADE
)


CREATE TABLE Answers (
  Report_ID 		INT		NOT NULL,
  QuestionNumber 	INT		NOT NULL,
  Answer 			TEXT 	NOT NULL,
  Result 			BIT	 	NOT NULL,
  FOREIGN KEY (Report_ID) REFERENCES Reports(Report_ID) ON DELETE CASCADE
)


CREATE TABLE PackageAccess (
  Access_ID 	INT		NOT NULL PRIMARY KEY IDENTITY(1,1),
  Student_ID 	INT		NOT NULL,
  Package_ID 	INT		NOT NULL,
  Access 		BIT		NOT NULL DEFAULT '0',
  FOREIGN KEY (Student_ID) REFERENCES Students(Student_ID) ON DELETE CASCADE,
  FOREIGN KEY (Package_ID) REFERENCES Packages(Package_ID) ON DELETE CASCADE
)


CREATE TABLE Questions (
  Question_ID	INT		 	NOT NULL PRIMARY KEY IDENTITY(1,1),
  Package_ID 	INT			NOT NULL,
  Number 		INT		 	NOT NULL,
  Type 			VARCHAR(20) NOT NULL,
  Question1 	TEXT 		NOT NULL,
  Question2 	VARCHAR(20) NOT NULL,
  Operation 	VARCHAR(10) NOT NULL,
  Answer 		TEXT 		NOT NULL,
  Media 		TEXT 		NOT NULL,
  Choice 		TEXT 		NOT NULL,
  FOREIGN KEY (Package_ID) REFERENCES Packages(Package_ID) ON DELETE CASCADE
);

--Reset ID Incrememt
DBCC CHECKIDENT (Teachers, RESEED, 0);

SELECT * FROM Teachers
DELETE FROM Teachers


INSERT INTO Teachers(Name, Username, Password, Address, PhoneNumber, Permission) 
VALUES ('Administrator', 'admin', 'admin', '192.168.11.0', '03172716610', 'Super Admin'),
	   ('Teacher 1', 'teacher1', 'teacher1', '192.168.11.1', '03172716611', 'Admin'),
	   ('Teacher 2', 'teacher2', 'teacher2', '192.168.11.2', '03172716612', 'Guru');









-----------------------------------------------------------------------------------------------------------------------------------
----CRUD
-----------------------------------------------------------------------------------------------------------------------------------
