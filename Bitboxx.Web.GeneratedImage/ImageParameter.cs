using System;
using System.ComponentModel;
using System.Web.UI;
using System.Globalization;

namespace Bitboxx.Web.GeneratedImage
{
    [Bindable(true)]
    public class ImageParameter : IDataBindingsAccessor {
        private DataBindingCollection _dataBindings = new DataBindingCollection();

        [Category("Data")]
        public string Name { get; set; }

        [Category("Data")]
        [Bindable(true)]
        public string Value { get; set; }

        public event EventHandler DataBinding;

        internal void DataBind() {
            if (DataBinding != null) {
                DataBinding(this, EventArgs.Empty);
            }
        }

        public override string ToString() {
            if (String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Value)) {
                return base.ToString();
            }
            else {
                return String.Format(CultureInfo.InvariantCulture, "{0} = {1}", Name, Value);
            }
        }

        [
        Bindable(false),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public Control BindingContainer {
            get;
            internal set;
        }

        #region IDataBindingsAccessor Members

        DataBindingCollection IDataBindingsAccessor.DataBindings {
            get {
                return _dataBindings;
            }
        }

        bool IDataBindingsAccessor.HasDataBindings {
            get {
                return _dataBindings.Count != 0;
            }
        }

        #endregion
    }
}
