using System.Collections.ObjectModel;
using WpfSelectItemInDoubleList.Utils;

namespace WpfSelectItemInDoubleList.Model
{
    /// <summary>
    /// An item that will be used as a container for nested item.
    /// </summary>
    public class TopItem : INPCBase
    {
        // The class MUST implement INotifyPropertyChanged in order
        // to notify that an item has changed in an ObservableCollection.

        private string _TopName;
        /// <summary>Name of the item.</summary>
        public string TopName
        {
            get { return this._TopName; }
            set
            {
                SetProperty(ref this._TopName, value);
            }
        }

        private ObservableCollection<NestedItem> _NestedItems;
        /// <summary>Collection of nested items.</summary>
        public ObservableCollection<NestedItem> NestedItems
        {
            get { return this._NestedItems; }
            set
            {
                SetProperty(ref this._NestedItems, value);
            }
        }

        private NestedItem _SelectedItem;
        /// <summary>
        /// Selected nested item. It simplifies a lot the work to do if this is present
        /// in the model: it's not easy to handle it only in the XAML.
        /// </summary>
        public NestedItem SelectedItem
        {
            get { return this._SelectedItem; }
            set
            {
                SetProperty(ref this._SelectedItem, value);
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="topName">Name to give to the item.</param>
        public TopItem(string topName)
        {
            TopName = topName;
        }
    }
}
