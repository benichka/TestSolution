using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfSelectItemInDoubleList.Utils
{
    /// <summary>
    /// Class that implements the INotifyPropertyChanged. Simplifies the boiler plate code
    /// to implement in classes that inherit from it. Usually, it's not a base class for everything
    /// (model, view model...). For the sake of this exercice, all classes that needs to implement
    /// the INotifyPropertyChanged will inherit from it.
    /// </summary>
    public abstract class INPCBase : INotifyPropertyChanged
    {
        /// <summary>Event handler.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raising the changed property event.
        /// </summary>
        /// <param name="propertyName">Changed property.</param>
        protected void RaisedPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Field change notification only if the field has really changed.
        /// </summary>
        /// <typeparam name="T">Field type.</typeparam>
        /// <param name="checkCommands">Action that checks the commands everytime a property changes.</param>
        /// <param name="storage">Initial value.</param>
        /// <param name="value">Updated value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>True if the field value changed, false otherwise.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            else
            {
                storage = value;
                RaisedPropertyChanged(propertyName);
                return true;
            }
        }
    }
}
