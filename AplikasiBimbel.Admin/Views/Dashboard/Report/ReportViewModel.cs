using System;
using System.ComponentModel;
using System.Windows.Media;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        #region Field

        public ReportModel Report { get; set; }
        public StudentModel Student { get; set; }
        public QuestionPackageModel Package { get; set; }
        public TimeSpan TimeSpent { get; }

        #endregion


        #region Constructor

        public ReportViewModel(ReportModel Report)
        {
            this.Report = Report;

            if (Report != null)
            {
                TimeSpent = Report.FinishTime - Report.StartTime;
            }
        } 

        #endregion


        #region Properties

        public char ScoreGrade {
            get {
                if (Report.Score >= 85) return 'A';
                else if (Report.Score >= 70) return 'B';
                else if (Report.Score >= 50) return 'C';
                else if (Report.Score >= 25) return 'D';
                else return 'E';

            }
        }

        public Brush Score_Foreground {
            get {

                if (Report.Score >= Package.MinimumScore)
                    return Brushes.Green;

                return Brushes.Red;
            }
        }

        #endregion


        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
