//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Common.Workflows.Activities {
    
    
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class ExtractZipArchive : System.Activities.Activity, System.ComponentModel.ISupportInitialize {
        
        private bool _contentLoaded;
        
        private System.Activities.InArgument<string> _sourceFileInfo;
        
        private System.Activities.InArgument<string> _extractionDirectoryInfo;
        
        private System.Activities.OutArgument<System.Collections.Generic.IEnumerable<System.IO.FileSystemInfo>> _result;
        
        public ExtractZipArchive() {
            this.InitializeComponent();
        }
        
        [System.Activities.RequiredArgumentAttribute()]
        public System.Activities.InArgument<string> sourceFileInfo {
            get {
                return this._sourceFileInfo;
            }
            set {
                this._sourceFileInfo = value;
            }
        }
        
        [System.Activities.RequiredArgumentAttribute()]
        public System.Activities.InArgument<string> extractionDirectoryInfo {
            get {
                return this._extractionDirectoryInfo;
            }
            set {
                this._extractionDirectoryInfo = value;
            }
        }
        
        public System.Activities.OutArgument<System.Collections.Generic.IEnumerable<System.IO.FileSystemInfo>> result {
            get {
                return this._result;
            }
            set {
                this._result = value;
            }
        }
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if ((this._contentLoaded == true)) {
                return;
            }
            this._contentLoaded = true;
            string resourceName = this.FindResource();
            System.IO.Stream initializeXaml = typeof(ExtractZipArchive).Assembly.GetManifestResourceStream(resourceName);
            System.Xml.XmlReader xmlReader = null;
            System.Xaml.XamlReader reader = null;
            System.Xaml.XamlObjectWriter objectWriter = null;
            try {
                System.Xaml.XamlSchemaContext schemaContext = XamlStaticHelperNamespace._XamlStaticHelper.SchemaContext;
                xmlReader = System.Xml.XmlReader.Create(initializeXaml);
                System.Xaml.XamlXmlReaderSettings readerSettings = new System.Xaml.XamlXmlReaderSettings();
                readerSettings.LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                readerSettings.AllowProtectedMembersOnRoot = true;
                reader = new System.Xaml.XamlXmlReader(xmlReader, schemaContext, readerSettings);
                System.Xaml.XamlObjectWriterSettings writerSettings = new System.Xaml.XamlObjectWriterSettings();
                writerSettings.RootObjectInstance = this;
                writerSettings.AccessLevel = System.Xaml.Permissions.XamlAccessLevel.PrivateAccessTo(typeof(ExtractZipArchive));
                objectWriter = new System.Xaml.XamlObjectWriter(schemaContext, writerSettings);
                System.Xaml.XamlServices.Transform(reader, objectWriter);
            }
            finally {
                if ((xmlReader != null)) {
                    ((System.IDisposable)(xmlReader)).Dispose();
                }
                if ((reader != null)) {
                    ((System.IDisposable)(reader)).Dispose();
                }
                if ((objectWriter != null)) {
                    ((System.IDisposable)(objectWriter)).Dispose();
                }
            }
        }
        
        private string FindResource() {
            string[] resources = typeof(ExtractZipArchive).Assembly.GetManifestResourceNames();
            for (int i = 0; (i < resources.Length); i = (i + 1)) {
                string resource = resources[i];
                if ((resource.Contains(".ExtractZipArchive.g.xaml") || resource.Equals("ExtractZipArchive.g.xaml"))) {
                    return resource;
                }
            }
            throw new System.InvalidOperationException("Resource not found.");
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033")]
        void System.ComponentModel.ISupportInitialize.BeginInit() {
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033")]
        void System.ComponentModel.ISupportInitialize.EndInit() {
            this.InitializeComponent();
        }
    }
}
