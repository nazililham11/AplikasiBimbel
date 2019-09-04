using AplikasiBimbel.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AplikasiBimbel.Controller
{
    public class StudentDatabaseHandle
    {

        #region Query Command

        //Get All Teacher Data
        //Shows Teacher_ID, Username, Name,  Permission, Status
        //Stored Procedure Name: sp_ReadAllTeacher
        public const string ReadAllTeacher = "sp_ReadAllStudent";

        //Get All Active Student
        //Shows Teacher_ID, Username, Name, Permission
        //Stored Procedure Name: sp_ReadActiveTeacher
        public const string ReadActiveTeacher = "sp_ReadActiveStudent";

        //Get Teacher Details By ID
        //Shows Name, Username, Password, Address, PhoneNumber, Permission, Status, DateIn, DateOut
        //Stored Procedure Name: sp_GetTeacherDetails 
        //Parameters : @teacher_id
        public const string GetTeacherDetails = "sp_GetStudentDetails";

        //Get Teacher Details By ID
        //Shows Name, Username, Password, Address, PhoneNumber, Permission, Status, DateIn, DateOut
        //Stored Procedure Name: sp_GetTeacherDetailsByUsername 
        //Parameters : @username
        public const string GetTeacherDetailsByUsername = "sp_GetTeacherDetailsByUsername";

        //Insert New Teacher
        //Default Value ID=Generated, Password=NULL, Status=Aktif, DateIn=Now
        //Stored Procedure Name: sp_InsertTeacher
        //Parameters : "@username , "@name , "@address , "@phoneNumber , "@permission 
        public const string InsertTeacher = "sp_InsertTeacher";

        //Generated new ID for New Teacher
        //Stored Procedure Name: sp_GenerateTeacherID
        public const string GenerateTeacherID = "sp_GenerateTeacherID";

        //Update Teacher Data
        //Stored Procedure Name: sp_UpdateTeacher 
        //Parameters : "@teacher_id , "@username , "@password , "@name , "@address , "@phonenumber , "@permission , "@status , "@dateout 
        public const string UpdateTeacher = "sp_UpdateTeacher";

        //Delete Teacher By ID
        //Stored Procedure Name: sp_DeleteTeacher 
        //Parameters : @teacher_id 
        public const string DeleteTeacher = "sp_DeleteTeacher";

        //Column Names
        public readonly string[] columnNames = { "Teacher_ID", "Username", "Password", "Name", "Address", "PhoneNumberr", "Permission", "Status", "DateIn", "DateOUt" };

        #endregion


        #region Prvate Member

        private static MSSQL_Connection connection;

        #endregion


        #region Constructor

        public StudentDatabaseHandle()
        {
            connection = new MSSQL_Connection();
        }

        #endregion


        public List<TeacherModel> ReadAll()
        {
            //CheckConnection();

            //List<TeacherModel> teacherlist = connection.ExecuteStoredProcedureQuery<TeacherModel>(ReadAllTeacher);
            List<TeacherModel> teacherlist = connection.ExecuteStoredProcedureQuery<TeacherModel>(ReadAllTeacher, columnNames);

            return teacherlist;
        }

        public List<TeacherModel> ReadActive()
        {
            //CheckConnection();

            List<TeacherModel> teacherlist = connection.ExecuteStoredProcedureQuery<TeacherModel>(ReadActiveTeacher, columnNames);

            return teacherlist;
        }

        public TeacherModel Find(int teacher_id)
        {
            //CheckConnection();

            SqlParameter[] param =
            {
                new SqlParameter("@teacher_id", teacher_id)
            };

            List<TeacherModel> teacherlist = connection.ExecuteStoredProcedureQuery<TeacherModel>(GetTeacherDetails, columnNames, param);
            if (teacherlist == null || teacherlist.Count < 1)
                return null;

            TeacherModel teacher = teacherlist[0];

            return teacher;
        }

        public TeacherModel Find(string username)
        {
            //CheckConnection();

            SqlParameter[] param =
            {
                new SqlParameter("@username", username)
            };

            List<TeacherModel> teacherlist = connection.ExecuteStoredProcedureQuery<TeacherModel>(GetTeacherDetails, columnNames, param);
            if (teacherlist == null || teacherlist.Count < 1)
                return null;

            TeacherModel teacher = teacherlist[0];

            return teacher;
        }
        public int GenerateID()
        {

            //CheckConnection();
            string newId = connection.ExecuteStoredProcedureScalar(GenerateTeacherID);

            if (string.IsNullOrEmpty(newId))
                return 0;

            int.TryParse(newId, out int id);

            return id;
        }

        public bool Add(TeacherModel teacher)
        {

            //CheckConnection();

            SqlParameter[] param =
            {
                new SqlParameter("@username", teacher.Username),
                new SqlParameter("@name", teacher.Name),
                new SqlParameter("@address", teacher.Address),
                new SqlParameter("@phonenumber", teacher.PhoneNumber),
                new SqlParameter("@permission", teacher.Permission)
            };

            int? result = connection.ExecuteStoredProcedureNonQuery(InsertTeacher, param);

            if (result == null || result < 1)
                return false;

            return true;
        }

        public bool Update(TeacherModel teacher)
        {

            //CheckConnection();

            SqlParameter[] param =
            {
                new SqlParameter("@teacher_id", teacher.Teacher_ID),
                new SqlParameter("@username", teacher.Username),
                new SqlParameter("@password", teacher.Password),
                new SqlParameter("@name", teacher.Name),
                new SqlParameter("@address", teacher.Address),
                new SqlParameter("@phonenumber", teacher.PhoneNumber),
                new SqlParameter("@permission", teacher.Permission),
                new SqlParameter("@status", teacher.Status),
                new SqlParameter("@dateout", teacher.DateOut)
            };

            int? result = connection.ExecuteStoredProcedureNonQuery(UpdateTeacher, param);

            if (result == null || result < 1)
                return false;

            return true;
        }

        public bool Delete(int teacher_id)
        {

            //CheckConnection();

            SqlParameter[] param =
            {
                new SqlParameter("@teacher_id", teacher_id)
            };

            int? result = connection.ExecuteStoredProcedureNonQuery(DeleteTeacher, param);

            if (result == null || result < 1)
                return false;

            return true;
        }


    }

}
