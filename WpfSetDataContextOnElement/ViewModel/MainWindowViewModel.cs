using System.Collections.ObjectModel;
using WpfSetDataContextOnElement.Model;

namespace WpfSetDataContextOnElement.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<CustomItem> _CustomItems;
        public ObservableCollection<CustomItem> CustomItems
        {
            get { return this._CustomItems; }
            set
            {
                SetProperty(ref this._CustomItems, value);
            }
        }

        private CustomItem _SelectedCustomItem;
        public CustomItem SelectedCustomItem
        {
            get { return this._SelectedCustomItem; }
            set
            {
                SetProperty(ref this._SelectedCustomItem, value);
            }
        }

        public MainWindowViewModel()
        {
            CustomItems = new ObservableCollection<CustomItem>()
            {
                new CustomItem() { ID = 1, Description = "Custom item 1", IsActive = true },
                new CustomItem() { ID = 2, Description = "Custom item 2", IsActive = false },
                new CustomItem() { ID = 3, Description = "Custom item 3", IsActive = true },
                new CustomItem() { ID = 4, Description = "Custom item 4", IsActive = false },
                new CustomItem() { ID = 5, Description = "Custom item 5", IsActive = true }
            };
        }
    }
}
