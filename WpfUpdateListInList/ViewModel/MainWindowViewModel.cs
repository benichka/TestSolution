using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfUpdateListInList.Command;

namespace WpfUpdateListInList.ViewModel
{
    public class MyItem : INotifyPropertyChanged
    {
        // The class MUST implement INotifyPropertyChanged in order
        // to notify that an item has changed in an ObservableCollection

        private string _Color;
        public string Color
        {
            get { return this._Color; }
            set
            {
                SetProperty(null, ref this._Color, value);
            }
        }

        public MyItem(string color)
        {
            Color = color;
        }

        #region event handling
        /// <summary>Event handler</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raising the changed property event
        /// </summary>
        /// <param name="propertyName">Changed property</param>
        protected void RaisedPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Field change notification only if the field has really changed
        /// </summary>
        /// <typeparam name="T">Field type</typeparam>
        /// <param name="checkCommands">Action that check the commands everytime a property changes</param>
        /// <param name="storage">Initial value</param>
        /// <param name="value">Updated value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>true if the field value changed, false otherwise</returns>
        protected bool SetProperty<T>(Action checkCommands, ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            else
            {
                storage = value;
                RaisedPropertyChanged(propertyName);
                checkCommands?.Invoke();
                return true;
            }
        }
        #endregion event handling
    }

    class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MyItem> MyItems { get; set; }

        public CommandHandler MakeFirstRedCommand { get; set; }

        public MainWindowViewModel()
        {
            MakeFirstRedCommand = new CommandHandler(MakeFirstRedClick, () => true);

            MyItems = new ObservableCollection<MyItem>();
            MyItems.Add(new MyItem("blue"));
            MyItems.Add(new MyItem("white"));
            MyItems.Add(new MyItem("red"));
        }

        public void MakeFirstRedClick()
        {
            MyItems[0].Color = "red";
        }

        public void CheckCommands()
        {
            MakeFirstRedCommand.RaiseCanExecuteChanged();
        }
    }
}
