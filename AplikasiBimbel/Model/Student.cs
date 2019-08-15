using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiBimbel.Model
{
    public class StudentModel
    {
        public int Student_ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public bool IsPasswordEnabled { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int AddedBy { get; set; }
        public DateTime DateIn { get; set; }
        public Nullable<DateTime> DateOut { get; set; }
    }
}
