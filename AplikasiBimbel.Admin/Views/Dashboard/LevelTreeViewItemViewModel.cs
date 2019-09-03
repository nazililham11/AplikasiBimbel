using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class LevelTreeViewItemViewModel : TreeViewItemViewModel
{
        public readonly LevelTreeViewItemModel _level;

        public string Name {
            get { return _level.Level; }
        }
        public int Count {
            get { return _level.Count; }
        }

        public LevelTreeViewItemViewModel(LevelTreeViewItemModel level, CourseTreeViewItemViewModel parent) : base(parent, true)
        {
            _level = level;
        }

        protected override void LoadChildren()
        {
            //foreach (PackageTreeViewItemModel package in App.PackageController.ReadPackage(_level.Course, _level.Level))
            //{
            //    base.Children.Add(new PackageTreeViewItemViewModel(package, this));
            //}
        }
    }
}
