using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Web.UI.Design;
using System.IO;
using System.Text.RegularExpressions;
using Bitboxx.Web.GeneratedImage.Resources;

namespace Bitboxx.Web.GeneratedImage
{
    // This control designer reads from the project file.
    [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class GeneratedImageDesigner : ControlDesigner {
        public override DesignerActionListCollection ActionLists {
            get {
                var actionLists = new DesignerActionListCollection();
                actionLists.AddRange(base.ActionLists);
                actionLists.Add(new ImageHandlerActionList(this));
                return actionLists;
            }
        }

        private void CreateImageHandler() {
            IWebApplication webApp = (IWebApplication)Component.Site.GetService(typeof(IWebApplication));
            
            GeneratedImage generatedImageControl = Component as GeneratedImage;

            if (generatedImageControl == null)
                return;

            try {
                var rootFolder = (IFolderProjectItem)webApp.RootProjectItem;
                var document = webApp.GetProjectItemFromUrl(this.RootDesigner.DocumentUrl);
                string template = GetHandlerTemplate(InferLanguage(document));

                var handlerDocument = rootFolder.AddDocument("ImageHandler1.ashx", ASCIIEncoding.Default.GetBytes(template));
                var handlerItem = handlerDocument as IProjectItem;

                // REVIEW setting the property directly on the control instance does not seem to work
                //generatedImageControl.ImageHandlerUrl = handlerItem.AppRelativeUrl;
                PropertyDescriptor disc = TypeDescriptor.GetProperties(generatedImageControl)["ImageHandlerUrl"];
                if (disc != null) {
                    disc.SetValue(generatedImageControl, handlerItem.AppRelativeUrl);
                }

                handlerDocument.Open();
            }
            catch (Exception) {
                // don't do anything, happens if a file existed and user chose not to override
            }
        }

        private static string GetHandlerTemplate(Language language) {
            switch (language) {
                case Language.CSharp:
                    return WebResources.ImageHandlerTemplate_CSharp;
                case Language.VB:
                    return WebResources.ImageHandlerTemplate_VB;
            }
            throw new InvalidOperationException("Invalid language detected");
        }

        private Language InferLanguage(IProjectItem document) {
            try {
                var language = InferLanguageFromContent(document);
                if (language != null) return language.Value;
            }
            catch (Exception) { }

            try {
                var language = InferLanguageFromCodebehindExtension(document);
                if (language != null) return language.Value;
            }
            catch (Exception) { }

            return Language.CSharp;
        }

        private static Language? InferLanguageFromContent(IProjectItem document) {
            using (var reader = new StreamReader(((IDocumentProjectItem)document).GetContents())) {
                var regex = new Regex(@"<%@.*Language=""(?<lang>[^""]*)"".*%>");
                string line;
                while ((line = reader.ReadLine()) != null) {
                    var match = regex.Match(line).Groups["lang"];
                    if (match.Success) {
                        switch (match.Value.ToLowerInvariant()) {
                            case "c#": return Language.CSharp;
                            case "vb": return Language.VB;
                        }
                    }
                }
            }
            return null;
        }

        private Language? InferLanguageFromCodebehindExtension(IProjectItem document) {
            var parentFolder = (IFolderProjectItem)document.Parent;

            foreach (IProjectItem siblingItem in parentFolder.Children) {
                var itemName = siblingItem.Name;
                if (itemName.StartsWith(document.Name)) {
                    if (itemName != document.Name) {
                        string extension = itemName.Substring(itemName.LastIndexOf('.') + 1).ToLowerInvariant();
                        switch (extension) {
                            case "cs":
                                return Language.CSharp;
                            case "vb":
                                return Language.VB;
                        }
                    }
                }
            }
            return null;
        }

        // Create the action list for this control.
        private class ImageHandlerActionList : DesignerActionList {
            private GeneratedImageDesigner Parent { get; set; }

            public ImageHandlerActionList(GeneratedImageDesigner parent)
                : base(parent.Component) {
                Parent = parent;
            }

            public void CreateImageHandler() {
                Parent.CreateImageHandler();
            }

            // Provide the list of sorted action items for the host.
            public override DesignerActionItemCollection GetSortedActionItems() {
                DesignerActionItemCollection items = new DesignerActionItemCollection();

                items.Add(new DesignerActionMethodItem(this,
                                               "CreateImageHandler",
                                               "Create Image Handler",
                                               "Create Handler",
                                               String.Empty,
                                               false));
                return items;
            }
        }

        private enum Language {
            CSharp, VB
        }
    }
}