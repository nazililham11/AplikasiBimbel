----Students-----

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



 GO
 CREATE PROCEDURE sp_ReadAllStudents AS
 BEGIN 
	SELECT Student_ID, Name, Nickname, Program, School, Grade, Status, IsPasswordEnabled
	FROM Students
END



GO
CREATE PROCEDURE sp_ReadActiveStudents AS
BEGIN 
	SELECT Student_ID, Name, Nickname, Program, School, Grade, Status, IsPasswordEnabled
	FROM Students 
	WHERE Status = 'Aktif'
END



GO
CREATE PROCEDURE sp_GetStudentDetails @student_id INT AS
BEGIN 
	SELECT Name, Nickname, Password, Program, School, Grade, Status, IsPasswordEnabled
	FROM Students 
	WHERE Student_ID = @student_id
END


GO
CREATE PROCEDURE sp_GenerateStudentID AS
BEGIN 
	SELECT MAX(Student_ID) + 1
	FROM Students
END



GO
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



GO
CREATE PROCEDURE sp_DeleteStudent @student_ID INT AS 
BEGIN
	DELETE FROM Students 
	WHERE Student_ID = @student_ID
END




---Teachers---

--Insert
GO
ALTER PROCEDURE sp_InsertTeacher 
  @username 	VARCHAR(30),
  @name 		VARCHAR(50),
  @address 		VARCHAR(100),
  @phoneNumber	VARCHAR(15),
  @permission 	VARCHAR(20)
 AS
 BEGIN
	INSERT INTO Teachers(Username, Name, Password, Address, PhoneNumber, Permission) 
	VALUES (@username, @name, NULL, @address, @phoneNumber, @permission)
 END
 --Example
 EXEC sp_InsertTeacher
  @username = 'teacher4', 
  @name = 'Teacher 4', 
  @address = '192.168.0.4', 
  @phoneNumber = '911', 
  @permission = 'Admin';



---Read All Teacher
GO
 CREATE PROCEDURE sp_ReadAllTeacher AS
 BEGIN 
	SELECT Teacher_ID, Username, Name,  Permission, Status
	FROM Teachers
END
--Example
EXEC sp_ReadAllTeacher

--Read All Active Student
GO
CREATE PROCEDURE sp_ReadActiveTeacher AS
BEGIN 
	SELECT Teacher_ID, Username, Name, Permission
	FROM Teachers
	WHERE Status = 'Aktif'
END
--Example
EXEC sp_ReadActiveTeacher


--Get Teacher Details
GO
CREATE PROCEDURE sp_GetTeacherDetails @teacher_id INT AS
BEGIN 
	SELECT Name, Username, Password, Address, PhoneNumber, Permission, Status, DateIn, DateOut
	FROM Teachers 
	WHERE Teacher_ID = @teacher_id
END
--Example
EXEC sp_GetTeacherDetails @teacher_id = '4'


--Get Teacher Details By Username
GO
CREATE PROCEDURE sp_GetTeacherDetailsByUsername @username VARCHAR(30) AS
BEGIN 
	SELECT Name, Username, Password, Address, PhoneNumber, Permission, Status, DateIn, DateOut
	FROM Teachers 
	WHERE Username = @username
END
--Example
EXEC sp_GetTeacherDetailsByUsername @username = 'admin'

--Generate Teacher ID
GO
CREATE PROCEDURE sp_GenerateTeacherID AS
BEGIN 
	SELECT MAX(Teacher_ID) + 1
	FROM Teachers
END
--Example
EXEC sp_GenerateTeacherID


--Upadte Teacher 
GO
CREATE PROCEDURE sp_UpdateTeacher
  @teacher_id		INT,
  @username 		VARCHAR(30),
  @password 		VARCHAR(30),
  @name				VARCHAR(50),
  @address 			VARCHAR(100),
  @phonenumber 		VARCHAR(40),
  @permission 		VARCHAR(20),
  @status 			VARCHAR(20),
  @dateout			DATETIME
AS
BEGIN
	UPDATE Teachers
	SET Username = @username,
		Password = @password,
		Name = @name,
		Address = @address,
		PhoneNumber = @phonenumber,
		Permission = @permission,
		Status = @status,
		DateOut = @dateout
	WHERE Teacher_ID = @teacher_id
END
--Example
EXEC sp_UpdateTeacher
  @teacher_id = 4,
  @username = 'teacher4',
  @password = NULL,
  @name = 'Teacherz',
  @address = '192.168.1.1',
  @phonenumber = '911',
  @permission = 'Admin',
  @status = 'Tidak Aktif',
  @dateout = '2018/08/07'



--Delete Teacher
GO
CREATE PROCEDURE sp_DeleteTeacher @teacher_id INT AS 
BEGIN
	DELETE FROM Teachers 
	WHERE Teacher_ID = @teacher_id
END
--Example
EXEC sp_DeleteTeacher @teacher_id = '4'

