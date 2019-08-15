using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplikasiBimbel.Model;


namespace AplikasiBimbel.Controller
{
    public class TeacherController
    {

        #region Field

        public const string SqlGetPersonEmailsCommand = "SELECT Email_Id, Email FROM Emails WHERE Person_Id = @Person_Id";
        public const string SqlUpdatePersonCommand = "UPDATE Person SET Name = @Name WHERE Id = @OriginalId";
        public const string SqlUpdatePersonEmailCommand = "UPDATE Emails SET Email = @Email WHERE Email_Id = @Email_Id";

        #endregion


        #region Constructor

        public TeacherController()
        {

        }

        #endregion


        #region Method

        /// <summary>
        /// Generate New Teacher ID
        /// </summary>
        /// <returns>Returns New Teacher ID</returns>
        public int? GenereateID()
        {
            string Query = "CALL sp_GenerateNewTeacherID()";

            int? value = App.Connection.ExecuteScalar(Query);

            if (value == null || value < 0)
                return null;

            return value;
        }

        /// <summary>
        /// Read Teacher By Teacher ID
        /// </summary>
        /// <param name="teacherID">Teacher ID</param>
        /// <returns>Return Teacher Data if Found and Returns Null if not Found</returns>
        public TeacherModel ReadByID(int teacherID = 0)
        {
            if (teacherID == 0)
                return null;

            string Query = $"SELECT * FROM Teachers WHERE Teacher_ID = '{teacherID}' LIMIT 1";

            List<TeacherModel> teachers = App.Connection.ExecuteQuery<TeacherModel>(Query, new TeacherModel().Entity);

            if (teachers == null || teachers.Count < 1)
                return null;

            return teachers[0];
        }

        /// <summary>
        /// Read Teacher By Username
        /// </summary>
        /// <param name="username">Teacher Username</param>
        /// <returns>Return Teacher Data if Found and Returns Null if not Found</returns>
        public TeacherModel ReadByUsername(string username = null)
        {
            if (username == null || username == string.Empty)
                return null;

            string Query = $"SELECT * FROM Teachers WHERE Username = '{username}' LIMIT 1";

            List<TeacherModel> teachers = App.Connection.ExecuteQuery<TeacherModel>(Query, new TeacherModel().Entity);

            if (teachers == null || teachers.Count < 1)
                return null;

            return teachers[0];
        }

        /// <summary>
        /// Read All Teacher
        /// </summary>
        /// <returns></returns>
        public List<TeacherModel> ReadAll()
        {
            string Query = "SELECT * FROM Teachers";
            List<TeacherModel> teachers = App.Connection.ExecuteQuery<TeacherModel>(Query, new TeacherModel().Entity);

            if (teachers == null)
                return new List<TeacherModel>();
            
            return teachers;
        }

        /// <summary>
        /// Create New Teacher Data
        /// </summary>
        /// <param name="data">New Teacher Data</param>
        /// <returns>Returns True if Update Completed</returns>
        public bool? Create(TeacherModel data)
        {
            if (data == null || data.Teacher_ID == 0)
                return null;

            string Query = $"INSERT INTO Teachers(Teacher_ID, Name, Username, Password, Address, PhoneNumber, Permission) " +
                           $"VALUES ('{data.Teacher_ID}', '{data.Name}', '{data.Username}', '{data.Password}', '{data.Address}', '{data.PhoneNumber}', '{data.Permission}');";

            int? result = App.Connection.ExecuteNonQuery(Query);

            if (result == null)
                return null;

            return result > 0;
        }

        /// <summary>
        /// Update Teacher Data
        /// </summary>
        /// <param name="data">Teacher Update Data</param>
        /// <returns>Returns True if Update Completed</returns>
        public bool? Update(TeacherModel data)
        {
            if (data == null || data.Teacher_ID == 0)
                return null;

            string Query = $"UPDATE Teachers SET Name = '{data.Name}', Username = '{data.Username}', Password = '{data.Password}', Address = '{data.Address}', " +
                           $"PhoneNumber = '{data.PhoneNumber}', Permission = '{data.Permission}' WHERE Teacher_ID = '{data.Teacher_ID.ToString()}';";

            int? result = App.Connection.ExecuteNonQuery(Query);

            if (result == null)
                return null;

            return result > 0;
        }

        #endregion
    }
}
