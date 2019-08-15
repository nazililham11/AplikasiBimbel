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
  IsPasswordEnabled BIT NOT NULL DEFAULT '0',
  DateAdded		DATETIME NOT NULL,
  DateNotActive DATETIME NULL,
)
GO
CREATE TABLE Teachers (
  Teacher_ID	INT		 	 NOT NULL PRIMARY KEY IDENTITY(1,1),
  Username 		VARCHAR(30)  NOT NULL,
  Password 		VARCHAR(30)  NULL,
  Name 			VARCHAR(50)  NOT NULL,
  Address 		VARCHAR(100) NOT NULL,
  PhoneNumber	VARCHAR(15)  NOT NULL,
  Permission 	VARCHAR(20)	 NOT NULL DEFAULT 'Guru' CHECK (Permission IN('Super Admin', 'Admin', 'Guru')),
  Status VARCHAR(20) NOT NULL DEFAULT 'Aktif' CHECK(Status IN('Aktif', 'Tidak Aktif'))
)
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














-----------------------------------------------------------------------------------------------------------------------------------
----CRUD
-----------------------------------------------------------------------------------------------------------------------------------
----Students
/**/

CREATE PROCEDURE sp_InsertStudents 
	@name VARCHAR(40), 
	@nickname VARCHAR(30), 
	@program TEXT, 
	@school VARCHAR(40), 
	@grade VARCHAR(20)
 AS
 BEGIN
	INSERT INTO Students(Name, Nickname, Password, Program, School, Grade) VALUES (@name, @nickname, NULL, @program, @school, @grade)
 END


 CREATE PROCEDURE sp_ReadAllStudents AS
 BEGIN 
	SELECT Student_ID, Name, Nickname, Program, School, Grade, Status, IsPasswordEnabled
	FROM Students
END


CREATE PROCEDURE sp_ReadActiveStudents AS
BEGIN 
	SELECT Student_ID, Name, Nickname, Program, School, Grade, Status, IsPasswordEnabled
	FROM Students 
	WHERE Status = 'Aktif'
END


CREATE PROCEDURE sp_GetDetails @student_id INT AS
BEGIN 
	SELECT Name, Nickname, Password, Program, School, Grade, Status, IsPasswordEnabled
	FROM Students 
	WHERE Student_ID = @student_id
END


CREATE PROCEDURE sp_GenerateStudentID AS
BEGIN 
	SELECT MAX(Student_ID) + 1
	FROM Students
END

CREATE PROCEDURE sp_UpdateStudent
  @student_ID		 INT,
  @name 			 VARCHAR(40),
  @nickname 		 VARCHAR(30),
  @password			 VARCHAR(30),
  @program 			 TEXT,
  @school 			 VARCHAR(40),
  @grade 			 VARCHAR(20),
  @status 			 VARCHAR(20),
  @isPasswordEnabled BIT
AS
BEGIN
	UPDATE Students
	SET Name = @name,
		Nickname = @nickname,
		Password = @password,
		Program = @program,
		School = @school,
		Grade = @grade,
		Status = @status,
		IsPasswordEnabled = @isPasswordEnabled
	WHERE Student_ID = @student_ID
END


CREATE PROCEDURE sp_DeleteStudent @student_ID INT AS 
BEGIN
	DELETE FROM Students 
	WHERE Student_ID = @student_ID
END



--Teachers
CREATE PROCEDURE sp_InsertTeacher 
  @username 	VARCHAR(30),
  @name 		VARCHAR(50),
  @address 		VARCHAR(100),
  @phoneNumber	VARCHAR(15),
  @permission 	VARCHAR(20)
 AS
 BEGIN
	INSERT INTO Teacher(Username, Name, Password, Address, PhoneNumber, Permission) VALUES (@username, @name, NULL, @address, @phoneNumber, @permission)
 END


 CREATE PROCEDURE sp_ReadAllTeacher AS
 BEGIN 
	SELECT Teacher_ID, Username, Address, PhoneNumber, Permission, Status
	FROM Teachers
END


CREATE PROCEDURE sp_ReadActiveTeacher AS
BEGIN 
	SELECT Teacher_ID, Username, Address, PhoneNumber, Permission, Status
	FROM Teachers
	WHERE Status = 'Aktif'
END


CREATE PROCEDURE sp_GetDetails @student_id INT AS
BEGIN 
	SELECT Name, Nickname, Password, Program, School, Grade Status, IsPasswordEnabled
	FROM Students 
	WHERE Student_ID = @student_id
END


CREATE PROCEDURE sp_GenerateStudentID AS
BEGIN 
	SELECT MAX(Student_ID) + 1
	FROM Students
END

CREATE PROCEDURE sp_UpdateStudent
  @student_ID		 INT,
  @name 			 VARCHAR(40),
  @nickname 		 VARCHAR(30),
  @password			 VARCHAR(30),
  @program 			 TEXT,
  @school 			 VARCHAR(40),
  @grade 			 VARCHAR(20),
  @status 			 VARCHAR(20),
  @isPasswordEnabled BIT
AS
BEGIN
	UPDATE Students
	SET Name = @name,
		Nickname = @nickname,
		Password = @password,
		Program = @program,
		School = @school,
		Grade = @grade,
		Status = @status,
		IsPasswordEnabled = @isPasswordEnabled
	WHERE Student_ID = @student_ID
END


CREATE PROCEDURE sp_DeleteStudent @student_ID INT AS 
BEGIN
	DELETE FROM Students 
	WHERE Student_ID = @student_ID
END

