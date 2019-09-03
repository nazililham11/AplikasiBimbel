using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AplikasiBimbel.Admin.Views.Dashboard
{
    public class TreeViewItemViewModel : INotifyPropertyChanged
    {

        static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();
        public ObservableCollection<TreeViewItemViewModel> Children { get; }
        public TreeViewItemViewModel Parent { get; }

        bool? _isChecked;
        bool _isExpanded;
        bool _isSelected;

        protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
        {
            Parent = parent;
            Children = new ObservableCollection<TreeViewItemViewModel>();
            if (lazyLoadChildren) Children.Add(DummyChild);
        }

        // Used to create the DummyChild instance.
        private TreeViewItemViewModel()
        {
        }


        public bool HasDummyChild {
            get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
        }


        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded {
            get { return _isExpanded; }
            set {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged(nameof(IsExpanded));
                }

                // Expand all the way up to the root.
                if (_isExpanded && Parent != null)
                    Parent.IsExpanded = true;

                // Lazy load the child items, if necessary.
                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
            }
        }



        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected {
            get { return _isSelected; }
            set {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }



        public bool? IsChecked {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
            {
                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
                if (Children != null)
                {
                    foreach (var item in this.Children)
                    {
                        item.SetIsChecked(_isChecked, true, false);
                    }
                }
            }

            if (updateParent && Parent != null)
            {
                Parent.VerifyCheckState();
            }

            this.OnPropertyChanged(nameof(IsChecked));
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.Children.Count; ++i)
            {
                bool? current = this.Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
