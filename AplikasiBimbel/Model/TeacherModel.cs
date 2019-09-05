using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplikasiBimbel.Controller;
using MySql.Data.MySqlClient;

namespace AplikasiBimbel.Model
{
    public class TeacherModel : IDatabaseEntity<TeacherModel>
    {
        public int Teacher_ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Permission { get; set; }
        public string Status { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }

        public TeacherModel Entity(IDataReader reader)
        {
            TeacherModel teacher = new TeacherModel
            {
                Teacher_ID = Convert.ToInt32(GetValue(reader, nameof(Teacher_ID))),
                Username = Convert.ToString(GetValue(reader, nameof(Username))),
                Password = Convert.ToString(GetValue(reader, nameof(Password))),
                Name = Convert.ToString(GetValue(reader, nameof(Name))),
                Address = Convert.ToString(GetValue(reader, nameof(Address))),
                PhoneNumber = Convert.ToString(GetValue(reader, nameof(PhoneNumber))),
                Permission = Convert.ToString(GetValue(reader, nameof(Permission))),
                Status = Convert.ToString(GetValue(reader, nameof(Status))),
                DateIn = Convert.ToDateTime(GetValue(reader, nameof(DateIn))),

                DateOut = Convert.ToDateTime(GetValue(reader, nameof(DateOut)))
            };

            return teacher;
        }

        public object GetValue(IDataReader reader, string columnName)
        {
            try
            {
                return reader[columnName];
            }
            catch
            {
                return null;
            }
            
        }

        public TeacherModel Entity(IDataReader reader, int[] orders)
        {
            TeacherModel teacher = new TeacherModel();

            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i] < 0 || reader.IsDBNull(orders[i]))
                    continue;

                switch (i)
                {
                    case 0:
                        teacher.Teacher_ID = reader.GetInt32(orders[i]);
                        break;
                    case 1:
                        teacher.Username = reader.GetString(orders[i]);
                        break;
                    case 2:
                        teacher.Password = reader.GetString(orders[i]);
                        break;
                    case 3:
                        teacher.Name = reader.GetString(orders[i]);
                        break;
                    case 4:
                        teacher.Address = reader.GetString(orders[i]);
                        break;
                    case 5:
                        teacher.PhoneNumber = reader.GetString(orders[i]);
                        break;
                    case 6:
                        teacher.Permission = reader.GetString(orders[i]);
                        break;
                    case 7:
                        teacher.Status = reader.GetString(orders[i]);
                        break;
                    case 8:
                        teacher.DateIn = reader.GetDateTime(orders[i]);
                        break;
                    case 9:
                        teacher.DateOut = reader.GetDateTime(orders[i]);
                        break;
                }

            }

            return teacher;
        }
        public override string ToString()
        {
            string teacherString = string.Empty;

            teacherString += $"{nameof(Teacher_ID)} = {Teacher_ID.ToString()}/n";
            teacherString += $"{nameof(Username)} = {Username ?? "NULL"}/n";
            teacherString += $"{nameof(Password)} = {Password ?? "NULL"}/n";
            teacherString += $"{nameof(Name)} = {Name ?? "NULL"}/n";
            teacherString += $"{nameof(Address)} = {Address ?? "NULL"}/n";
            teacherString += $"{nameof(PhoneNumber)} = {PhoneNumber ?? "NULL"}/n";
            teacherString += $"{nameof(Permission)} = {Permission ?? "NULL"}/n";
            teacherString += $"{nameof(Status)} = {Status ?? "NULL"}/n";
            teacherString += $"{nameof(DateIn)} = {((DateIn == null) ? DateIn.ToLongDateString() : "NULL")}/n";
            teacherString += $"{nameof(DateOut)} = {((DateOut == null) ? DateOut.Value.ToLongDateString() : "NULL")}/n";
                  
            return teacherString;
        }
        

    }
}


