using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class CourseTreeViewItemViewModel : TreeViewItemViewModel
    {
        public readonly CourseTreeViewItemModel _course;

        public string Name {
            get { return _course.Course; }
        }
        public int Count {
            get { return _course.Count; }
        }

        public CourseTreeViewItemViewModel(CourseTreeViewItemModel course) : base(null, true)
        {
            _course = course;
        }

        protected override void LoadChildren()
        {
            //foreach (LevelTreeViewItemModel level in App.PackageController.ReadLevel(_course.Course))
            //{
            //    base.Children.Add(new LevelTreeViewItemViewModel(level, this));
            //}
        }
    }
}
