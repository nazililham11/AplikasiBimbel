using AplikasiBimbel.Model;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class PackageTreeViewItemViewModel : TreeViewItemViewModel
    {
        public readonly PackageTreeViewItemModel _package;
        public int Package_ID {
            get {
                return int.TryParse(_package.Package_ID, out int package_id) ? package_id : 0;
            }
        }
        public int Access_ID { get; set; }
        public string Name {
            get { return _package.Package; }
        }

        public PackageTreeViewItemViewModel(PackageTreeViewItemModel package, PackageTreeViewItemViewModel parent) : base(parent, false)
        {
            _package = package;
            Access_ID = 0;
        }

    }
}
