namespace AplikasiBimbel.Model
{
    public class QuestionPackageModel
    {
        public int Package_ID { get; set; }
        public string Course { get; set; }
        public string Level { get; set; }
        public string Package { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public double MinimumScore { get; set; }
    }
}
