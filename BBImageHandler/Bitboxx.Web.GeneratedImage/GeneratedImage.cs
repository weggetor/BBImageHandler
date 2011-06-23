using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bitboxx.Web.GeneratedImage 
{
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Designer(typeof(GeneratedImageDesigner))]
    public class GeneratedImage : Image {
        private const string s_timestampField = "__timestamp";
        private string _timestamp;
        private Control _bindingContainer;
        private readonly HttpContextBase _context;
        private string _imageHandlerUrl;

        [DefaultValue("")]
        [Category("Behavior")]
        [UrlProperty("*.ashx")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ImageHandlerUrl {
            get {
                return _imageHandlerUrl ?? String.Empty;
            }
            set {
                _imageHandlerUrl = value;
            }
        }

        [Bindable(true)]
        [DefaultValue("")]
        [Category("Data")]
        public string Timestamp {
            get {
                return _timestamp ?? String.Empty;
            }
            set {
                _timestamp = value;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(null)]
        [Category("Data")]
        [MergableProperty(false)]
        public List<ImageParameter> Parameters {
            get;
            private set;
        }

        private new HttpContextBase Context {
            get {
                return _context ?? new HttpContextWrapper(HttpContext.Current);
            }
        }

        private new Control BindingContainer {
            get {
                return _bindingContainer ?? base.BindingContainer;
            }
        }

        public GeneratedImage() {
            Parameters = new List<ImageParameter>();
        }

        internal GeneratedImage(HttpContextBase context, Control bindingContainer)
            : this() {
            _context = context;
            _bindingContainer = bindingContainer;
        }

        protected override void OnDataBinding(EventArgs e) {
            base.OnDataBinding(e);

            Control bindingContainer = BindingContainer;
            foreach (var parameter in Parameters) {
                parameter.BindingContainer = bindingContainer;
                parameter.DataBind();
            }
        }

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            if (DesignMode) {
                return;
            }

            ImageUrl = BuildImageUrl();
        }

        private string BuildImageUrl() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(ImageHandlerUrl);

            bool paramAlreadyAdded = false;

            foreach (var parameter in Parameters) {
                AddQueryStringParameter(stringBuilder, paramAlreadyAdded, parameter.Name, parameter.Value);
                paramAlreadyAdded = true;
            }

            if (Timestamp != null) {
                string timeStamp = Timestamp.Trim();
                if (!String.IsNullOrEmpty(timeStamp)) {
                    AddQueryStringParameter(stringBuilder, paramAlreadyAdded, s_timestampField, timeStamp);
                    paramAlreadyAdded = true;
                }
            }

            return stringBuilder.ToString();
        }

        private static void AddQueryStringParameter(StringBuilder stringBuilder, bool paramAlreadyAdded, string name, string value) {
            if (paramAlreadyAdded) {
                stringBuilder.Append('&');
            }
            else {
                stringBuilder.Append('?');
            }
            stringBuilder.Append(HttpUtility.UrlEncode(name));
            stringBuilder.Append('=');
            stringBuilder.Append(HttpUtility.UrlEncode(value));
        }
    }
}
