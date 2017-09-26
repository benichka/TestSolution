using System.Collections.ObjectModel;
using WpfSelectItemInDoubleList.Command;
using WpfSelectItemInDoubleList.Model;
using WpfSelectItemInDoubleList.Utils;

namespace WpfSelectItemInDoubleList.ViewModel
{
    /// <summary>
    /// ViewModel for the main window.
    /// </summary>
    public class MainWindowViewModel : INPCBase
    {
        /// <summary>Command to select a top item.</summary>
        public CommandHandler<TopItem> SelectTopItemCommand { get; set; }

        private ObservableCollection<TopItem> _TopItems;
        /// <summary>Top items collections.</summary>
        public ObservableCollection<TopItem> TopItems
        {
            get { return this._TopItems; }
            set
            {
                SetProperty(ref this._TopItems, value);
            }
        }

        private TopItem _SelectedTopItem;
        /// <summary>
        /// Selected top item. Like in the TopItem class, it simplifies a lot the work to do if this is present
        /// in the view model: it's not easy to handle it only in the XAML.
        /// </summary>
        public TopItem SelectedTopItem
        {
            get { return this._SelectedTopItem; }
            set
            {
                SetProperty(ref this._SelectedTopItem, value);
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindowViewModel()
        {
            // Initialises the command.
            SelectTopItemCommand = new CommandHandler<TopItem>(SelectTopItem, () => true);

            // Initialises the top items collection and populate it with some data.
            TopItems = new ObservableCollection<TopItem>();

            for (int i = 0; i < 5; i++)
            {
                var topItem = new TopItem($"top item {i}")
                {
                    NestedItems = new ObservableCollection<NestedItem>()
                };

                for (int j = 0; j < 5; j++)
                {
                    var nestedItem = new NestedItem($"NI {j}");

                    topItem.NestedItems.Add(nestedItem);
                }

                TopItems.Add(topItem);
            }
        }

        /// <summary>
        /// Mark a top item as the selected one in the collection.
        /// </summary>
        /// <param name="topItem">The top item to mark as selected.</param>
        private void SelectTopItem(TopItem topItem)
        {
            SelectedTopItem = topItem;
        }
    }
}
