using WpfSelectItemInDoubleList.Utils;

namespace WpfSelectItemInDoubleList.Model
{
    /// <summary>
    /// An item that will be in a collection in a top item.
    /// </summary>
    public class NestedItem : INPCBase
    {
        private string _NestedName;
        /// <summary>Name of the item.</summary>
        public string NestedName
        {
            get { return this._NestedName; }
            set
            {
                SetProperty(ref this._NestedName, value);
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nestedName">Name to give to the item.</param>
        public NestedItem(string nestedName)
        {
            NestedName = nestedName;
        }
    }
}
