﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bitboxx.Web.GeneratedImage.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class WebResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WebResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Bitboxx.Web.GeneratedImage.Resources.WebResources", typeof(WebResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;%@ WebHandler Language=&quot;C#&quot; Class=&quot;ImageHandler1&quot; %&gt;
        ///
        ///using System;
        ///using System.Collections.Specialized;
        ///using System.Drawing;
        ///using System.Web;
        ///using Microsoft.Web;
        ///
        ///public class ImageHandler1 : ImageHandler {
        ///    
        ///    public ImageHandler1() {
        ///        // Set caching settings and add image transformations here
        ///        // EnableServerCache = true;
        ///    }
        ///    
        ///    public override ImageInfo GenerateImage(NameValueCollection parameters) {
        ///        // Add image generation logic here and return an [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ImageHandlerTemplate_CSharp {
            get {
                return ResourceManager.GetString("ImageHandlerTemplate_CSharp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;%@ WebHandler Language=&quot;VB&quot; Class=&quot;ImageHandler1&quot; %&gt;
        ///
        ///Imports System
        ///Imports System.Collections.Specialized
        ///Imports System.Drawing
        ///Imports System.Web
        ///Imports Microsoft.Web
        ///
        ///Public Class ImageHandler1
        ///    Inherits ImageHandler
        ///    
        ///    Public Sub New()
        ///        MyBase.New
        ///        &apos;Set caching settings and add image transformations here
        ///        &apos;EnableServerCache = True
        ///    End Sub
        ///    
        ///    Public Overrides Function GenerateImage(ByVal parameters As NameValueCollection) As ImageInfo
        ///         [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ImageHandlerTemplate_VB {
            get {
                return ResourceManager.GetString("ImageHandlerTemplate_VB", resourceCulture);
            }
        }
    }
}