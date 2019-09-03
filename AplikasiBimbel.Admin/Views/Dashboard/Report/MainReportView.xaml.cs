using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AplikasiBimbel.Model;
using AplikasiBimbel.Admin.Properties;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for MainReportView.xaml
    /// </summary>
    public partial class MainReportView : UserControl, INotifyPropertyChanged
    {

        #region Field

        private ObservableCollection<ReportViewModel> _reports;

        #endregion


        #region Constructor

        public MainReportView()
        {
            InitializeComponent();

            DatePicker_DateStartFilter.DataContext = this;
            DatePicker_DateEndFilter.DataContext = this;

            LoadColumnShow();
            LoadMenuItem();

        }

        #endregion

        #region Properties

        public ObservableCollection<ReportViewModel> Reports {
            get {
                if (_reports == null)
                    _reports = new ObservableCollection<ReportViewModel>();

                return _reports;
            }
            set {
                _reports = value;
                OnPropertyChanged(nameof(Reports));
            }
        }

        public List<int> ColumnShow { get; set; }

        #endregion


        #region Method

        public void LoadStudentFilterList()
        {
            //Clear Student Combobox Filter Item
            Combobox_StudentIDFilter.Items.Clear();
            //Add The Select All Item
            Combobox_StudentIDFilter.Items.Add(new ComboBoxItem() { Content = "All Student", Tag = 0, ToolTip = "Select All Students" });
            //Select First Item - Select All
            Combobox_StudentIDFilter.SelectedIndex = 0;

            //TODO:Fix This
            //Read Students List
            Dictionary<int, string> students = new Dictionary<int, string>(); //App.StudentController.ReadStudentNickname();

            //Check Student List
            if (students == null)
                return;
            
            //Add To Combobox
            foreach (KeyValuePair<int, string> item in students)
            {
                Combobox_StudentIDFilter.Items.Add(new ComboBoxItem() {
                    Content = item.Value,
                    Tag = item.Key,
                    ToolTip = item.Value
                });
            }

            //Dispose The students 
            students = null;
        }

        public void LoadPackageFilterList()
        {
            //Clear Student Combobox Filter Item
            Combobox_PackageIDFilter.Items.Clear();
            //Add Item For Select All
            Combobox_PackageIDFilter.Items.Add(new ComboBoxItem() { Content = "All Package", Tag = 0, ToolTip = "Select All Package" });
            //Select Item 0 - Select All
            Combobox_PackageIDFilter.SelectedIndex = 0;

            //TODO:Fix This
            //Read Students List
            List<QuestionPackageModel> packages = new List<QuestionPackageModel>(); //App.PackageController.ReadPackageName();

            //Check The Package Value
            if (packages == null)
                return;

            //Add package to Combobox
            foreach (QuestionPackageModel item in packages)
            {

                string header = $"{item.Package} - {item.Level} - {item.Course}";

                Combobox_PackageIDFilter.Items.Add( new ComboBoxItem()
                {
                    Content = header,
                    Tag = item.Package_ID,
                    ToolTip = header
                });
            }

            //Dispose The package 
            packages = null;
        }

        public void LoadColumnShow()
        {
            //Read Column Shows from Settings
            ColumnShow = new List<int>(); //Settings.Default.ReportTableColumnShow ?? new List<int>();

            if (ColumnShow.Count > 0)
            {
                //Set All Column To Collapsed
                foreach (var item in DataGrid_Report.Columns)
                    item.Visibility = Visibility.Collapsed;

                //Set the Column to Shows to Visible
                foreach (int item in ColumnShow)
                    DataGrid_Report.Columns[item].Visibility = Visibility.Visible;
            }
            else
            {
                //Show All Column
                for (int i = 0; i < DataGrid_Report.Columns.Count; i++)
                    ColumnShow.Add(i);
            }

        }

        public void LoadStudentsReport(string DateStart = null, string DateEnd = null, string Student_ID = null, string Package_ID = null)
        {
            //TODO: Fix This
            //Read Reports
            List<ReportViewModel> reports = new List<ReportViewModel>();//App.ReportController.Read(DateStart, DateEnd, Student_ID, Package_ID);

            //Check reports value
            if (reports == null)
                return;

            //Add report item to Table
            foreach (var item in reports)
                Reports.Add(item);
            
            //Dispose reports
            reports = null;
        }

        #endregion


        #region Events

        private void StudentsReport_Loaded(object sender, RoutedEventArgs e)
        {
            //Read Student List if Empty
            if (Combobox_StudentIDFilter.Items.Count < 1)
                LoadStudentFilterList();

            //Read Package List if Empty
            if (Combobox_PackageIDFilter.Items.Count < 1)
                LoadPackageFilterList();

            //Read Report if Empty
            if (Reports.Count < 1)
                LoadStudentsReport();
        }

        private void LoadMenuItem()
        {
            //Get The Column Shows Context Menu
            ContextMenu menu = DataGrid_Report.FindResource("DataGridColumnHeaderContextMenu") as ContextMenu;

            //Add Menu Item to Column Shows Context Menu from Datagrid Column
            foreach (DataGridColumn item in DataGrid_Report.Columns)
            {
                int index = 0;
                MenuItem menuItem = new MenuItem()
                {
                    Header = item.Header,
                    Tag = index++,
                    IsCheckable = true,
                    IsChecked = item.Visibility == Visibility.Visible
                };

                //Add The Click Events
                menuItem.Click += MenuItem_Click;
                //Add To menu
                menu.Items.Add(menuItem);
            }
        }

        private void Button_Hasil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //return if sender type is not MenuItem
            if (!(sender is MenuItem))
                return;
            
            MenuItem item = sender as MenuItem;

            //Get Column Order From Tag
            if (!int.TryParse(item.Tag.ToString(), out int index))
                return;

            //Check The Items.IsCHecked Property
            if (item.IsChecked)
            {
                ColumnShow.Add(index);
                DataGrid_Report.Columns[index].Visibility = Visibility.Visible;
            } else {
                ColumnShow.Remove(index);
                DataGrid_Report.Columns[index].Visibility = Visibility.Collapsed;
            }

            //Save Column Shows Changes to Settings
            //Settings.Default["ReportTableColumnShow"] = ColumnShow;
            
        }

        private void Button_Filter_Click(object sender, RoutedEventArgs e)
        {
            string DateStart = null;
            string DateEnd = null;
            string Student_ID = null;
            string Package_ID = null;

            Reports = new ObservableCollection<ReportViewModel>();
            
            //Get The Time Filter
            if (Combobox_TimeFilter.SelectedIndex == 0)
            {
                //Today
                DateStart = DateTime.Now.ToString("yyyy-M-d"); ;
            }
            else if (Combobox_TimeFilter.SelectedIndex == 1)
            {
                //Last Week
                DateStart = DateTime.Now.AddDays(-7).ToString("yyyy-M-d");
            }
            else if (Combobox_TimeFilter.SelectedIndex == 2)
            {
                //Last Month
                DateStart = DateTime.Now.AddMonths(-1).ToString("yyyy-M-d");
            }
            else if (Combobox_TimeFilter.SelectedIndex == 3)
            {
                //Select All
                DateStart = null;
            }
            else if (Combobox_TimeFilter.SelectedIndex == 4)
            {
                //Custom Date Filter

                if (DatePicker_DateStartFilter.SelectedDate != null)
                {
                    //Date Start
                    DateStart = DatePicker_DateStartFilter.SelectedDate.Value.ToString("yyyy-M-d");
                }

                if (DatePicker_DateEndFilter.SelectedDate != null)
                {
                    //Date End
                    DateEnd = DatePicker_DateEndFilter.SelectedDate.Value.ToString("yyyy-M-d");
                }
            }

            //Get The Student Filter
            if (Combobox_StudentIDFilter.SelectedItem is ComboBoxItem student)
            {
                if (int.TryParse(student.Tag.ToString(), out int id))
                    if (id > 0) Student_ID = id.ToString();
            }

            //Get The Package Filter
            if (Combobox_PackageIDFilter.SelectedItem is ComboBoxItem package)
            {
                if (int.TryParse(package.Tag.ToString(), out int id))
                    if (id > 0) Package_ID = id.ToString();
            }
           
            //Read The Report With FIlters
            LoadStudentsReport(DateStart, DateEnd, Student_ID, Package_ID);
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
