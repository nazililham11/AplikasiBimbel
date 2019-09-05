using AplikasiBimbel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplikasiBimbel.Controller
{
    public class TeacherController
    {


        #region Private Member

        private readonly string _connectionString;

        #region Query Command

        //Get All Teacher Data
        //Shows Teacher_ID, Username, Name,  Permission, Status
        //Stored Procedure Name: sp_ReadAllTeacher
        public const string ReadAllTeacher = "sp_ReadAllTeacher";

        //Get All Active Student
        //Shows Teacher_ID, Username, Name, Permission
        //Stored Procedure Name: sp_ReadActiveTeacher
        public const string ReadActiveTeacher = "sp_ReadActiveTeacher";

        //Get Teacher Details By ID
        //Shows Username, Password, Name, Address, PhoneNumber, Permission, Status, DateIn, DateOut
        //Stored Procedure Name: sp_GetTeacherDetails 
        //Parameters : @teacher_id
        public const string GetTeacherDetails = "sp_GetTeacherDetails";

        //Get Teacher Details By ID
        //Shows Username, Password, Name, Address, PhoneNumber, Permission, Status, DateIn, DateOut
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

        #endregion

        #endregion


        #region Constructor

        public TeacherController(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public TeacherController()
        {
            this._connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseBimbel;Integrated Security=True;";
        }
        #endregion


        #region Method

        //Get All Teacher Data
        //Shows Teacher_ID, Username, Name,  Permission, Status
        //Stored Procedure Name: sp_ReadAllTeacher//Shows Teacher_ID, Username, Name,  Permission, Status
        public List<TeacherModel> ReadAll()
        {
            List<TeacherModel> teacherList = new List<TeacherModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = ReadAllTeacher;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        //Open Connection
                        connection.Open();

                        //Execute The Query
                        SqlDataReader result = command.ExecuteReader();

                        //Fetch Result
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                teacherList.Add(new TeacherModel
                                {
                                    Teacher_ID = result.GetInt32(0),
                                    Username = result.GetString(1),
                                    Name = result.GetString(2),
                                    Permission = result.GetString(3),
                                    Status = result.GetString(4)
                                });
                            }
                        }

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return teacherList;
        }


        //Get All Active Student
        //Shows Teacher_ID, Username, Name, Permission
        //Stored Procedure Name: sp_ReadActiveTeacher
        public List<TeacherModel> ReadActive()
        {
            List<TeacherModel> teacherList = new List<TeacherModel>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = ReadActiveTeacher;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        //Open Connection
                        connection.Open();

                        //Execute The Query
                        SqlDataReader result = command.ExecuteReader();

                        //Fetch Result
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                teacherList.Add(new TeacherModel
                                {
                                    Teacher_ID = result.GetInt32(0),
                                    Username = result.GetString(1),
                                    Name = result.GetString(2),
                                    Permission = result.GetString(3)
                                });
                            }
                        }

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return teacherList;
        }


        //Get Teacher Details By ID
        //Shows Name, Username, Password, Address, PhoneNumber, Permission, Status, DateIn, DateOut
        //Stored Procedure Name: sp_GetTeacherDetails 
        //Parameters : @teacher_id
        private TeacherModel _find(int teacher_id = 0, string username = null)
        {
            TeacherModel teacher = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        //Check Parameters
                        if (!string.IsNullOrEmpty(username))
                        {
                            command.CommandText = GetTeacherDetailsByUsername;
                            command.Parameters.AddWithValue("@username", username);
                        }
                        else
                        {
                            command.CommandText = GetTeacherDetails;
                            command.Parameters.AddWithValue("@teacher_id", teacher_id);
                        }

                        //Open Connection
                        connection.Open();

                        //Execute The Query
                        SqlDataReader result = command.ExecuteReader();

                        //Fetch Result
                        if (result.HasRows)
                        {
                            result.Read();

                            teacher = new TeacherModel()
                            {
                                Teacher_ID = teacher_id,
                                Username = result.GetString(0),
                                Password = result.IsDBNull(1) ? null : result.GetString(1),
                                Name = result.GetString(2),
                                Address = result.GetString(3),
                                PhoneNumber = result.GetString(4),
                                Permission = result.GetString(5),
                                Status = result.GetString(6),
                                DateIn = result.GetDateTime(7),
                                DateOut = result.IsDBNull(8) ? null : (DateTime?)result.GetDateTime(8)
                            };

                        }

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return teacher;
        }
        public TeacherModel Find(int teacher_id)
        {
            return _find(teacher_id: teacher_id);
        }
        public TeacherModel Find(string username)
        {
            return _find(username: username);
        }


        //Insert New Teacher
        //Default Value ID=Generated, Password=NULL, Status=Aktif, DateIn=Now
        //Stored Procedure Name: sp_InsertTeacher
        //Parameters : "@username , "@name , "@address , "@phoneNumber , "@permission 
        public bool Insert(TeacherModel teacher)
        {
            int rowAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = InsertTeacher;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Open Connection
                        connection.Open();

                        //Fill The Parameters
                        command.Parameters.AddWithValue("@username", teacher.Username);
                        command.Parameters.AddWithValue("@name", teacher.Name);
                        command.Parameters.AddWithValue("@address", teacher.Address);
                        command.Parameters.AddWithValue("@phonenumber", teacher.PhoneNumber);
                        command.Parameters.AddWithValue("@permission", teacher.Permission);

                        //Execute The Query
                        rowAffected = command.ExecuteNonQuery();

                        //Close the Connection
                        connection.Close();

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            return rowAffected > 0;
        }


        //Update Teacher Data
        //Stored Procedure Name: sp_UpdateTeacher 
        //Parameters : "@teacher_id , "@username , "@password , "@name , "@address , "@phonenumber , "@permission , "@status , "@dateout 
        public bool Update(TeacherModel teacher)
        {
            int rowAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = UpdateTeacher;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Open Connection
                        connection.Open();

                        //Fill The Parameters
                        command.Parameters.AddWithValue("@teacher_id", teacher.Teacher_ID);
                        command.Parameters.AddWithValue("@username", teacher.Username);
                        command.Parameters.AddWithValue("@password", teacher.Password);
                        command.Parameters.AddWithValue("@name", teacher.Name);
                        command.Parameters.AddWithValue("@address", teacher.Address);
                        command.Parameters.AddWithValue("@phonenumber", teacher.PhoneNumber);
                        command.Parameters.AddWithValue("@permission", teacher.Permission);
                        command.Parameters.AddWithValue("@status", teacher.Status);
                        command.Parameters.AddWithValue("@dateout", teacher.DateOut);

                        //Execute The Query
                        rowAffected = command.ExecuteNonQuery();

                        //Close the Connection
                        connection.Close();

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return rowAffected > 0;
        }


        //Delete Teacher By ID
        //Stored Procedure Name: sp_DeleteTeacher 
        //Parameters : @teacher_id 
        public bool Delete(int teacher_id)
        {
            int rowAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = DeleteTeacher;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        // Open Connection
                        connection.Open();

                        //Fill The Parameters
                        command.Parameters.AddWithValue("@teacher_id", teacher_id);

                        //Execute The Query
                        rowAffected = command.ExecuteNonQuery();

                        //Close the Connection
                        connection.Close();

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return rowAffected > 0;
        }


        //Generated new ID for New Teacher
        //Stored Procedure Name: sp_GenerateTeacherID
        public int GenerateNewID()
        {
            int newId = 0;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        //Create Command With Existing Connection with Stored Procedure Type
                        command.Connection = connection;
                        command.CommandText = GenerateTeacherID;
                        command.CommandTimeout = 60;
                        command.CommandType = CommandType.StoredProcedure;

                        //Open Connection
                        connection.Open();

                        //Execute The Query
                        string result = command.ExecuteScalar().ToString();

                        //Check the Result
                        if (int.TryParse(result, out newId))
                            return 0;

                        //Close the Connection
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + "\nError Code:" + ex.Number + "ConString: " + _connectionString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\nMessage:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return newId;
        } 


        #endregion
    }
}
