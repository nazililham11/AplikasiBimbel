using System;

namespace AplikasiBimbel.Model
{
    public class ReportModel
    {
        public int Report_ID { get; set; }
        public int Student_ID { get; set; }
        public int Package_ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Status { get; set; }
        public float Score { get; set; }
    }
}
