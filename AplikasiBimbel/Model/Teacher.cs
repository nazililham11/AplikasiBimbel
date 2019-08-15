using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AplikasiBimbel.Model
{
    public class TeacherModel
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

        public TeacherModel Entity(MySqlDataReader reader)
        {
            TeacherModel data = new TeacherModel()
            {
                Teacher_ID = int.TryParse(reader["Teacher_ID"].ToString(), out int teacherID) ? teacherID : 0,
                Name = reader["Name"].ToString() + string.Empty,
                Username = reader["Username"].ToString() + string.Empty,
                Password = reader["Password"].ToString() + string.Empty,
                Address = reader["Address"].ToString() + string.Empty,
                PhoneNumber = reader["PhoneNumber"].ToString() + string.Empty,
                Permission = reader["Permission"].ToString() + string.Empty
            };

            return data;
        }

    }
}
