using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Diagnostics.Contracts;
namespace Common.Utils.Extensions {
  public static class XmlExtensions {
    public static void CopyObject( this object source, Type type, object destination ) {
      if( type.IsInterface ) {
        PropertyInfo[] myInterfaceFields = type.GetProperties(
        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );

        foreach( PropertyInfo fi in myInterfaceFields ) {
          fi.SetValue( destination, fi.GetValue( source, null ), null );
        }
      }
      FieldInfo[] myObjectFields = type.GetFields(
        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );

      foreach( FieldInfo fi in myObjectFields ) {
        fi.SetValue( destination, fi.GetValue( source ) );
      }
    }
    public static XElement SerializeDataContract( this object target ) {
      var settings = new System.Xml.XmlWriterSettings();
      settings.Indent = true;
      settings.OmitXmlDeclaration = true;

      var sb = new System.Text.StringBuilder();
      using( var writer = System.Xml.XmlWriter.Create( sb, settings ) ) {
        var serializer = new System.Runtime.Serialization.DataContractSerializer( target.GetType() );
        serializer.WriteObject( writer, target );
        writer.Flush();
      }

      return XElement.Parse( sb.ToString() );
    }
    public static XElement Serialize( this object target ) {
      {
        StringBuilder builder = new StringBuilder();
        System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create( builder );
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer( target.GetType() );
        serializer.Serialize( xmlWriter, target );
        return XElement.Parse( builder.ToString() );
      };
    }
    public static XElement Transform( this XElement element, FileInfo xslFileInfo ) {
      return element.Transform( new XsltArgumentList(), xslFileInfo );
    }
    public static XElement Transform( this XElement element, XsltArgumentList args, FileInfo xslFileInfo,bool debug ) {
      var document = new XDocument();
      document.Add( element );
      return document.Transform( args, xslFileInfo ).Root;
    }
    public static XElement Transform( this XElement element,XsltArgumentList args, FileInfo xslFileInfo ) {
      return element.Transform( args, xslFileInfo, false );
    }
    public static XDocument Transform( this XDocument document, FileInfo xslFileInfo ) {
      return document.Transform( new XsltArgumentList(), xslFileInfo );
    }
    public static XDocument Transform( this XDocument document, FileInfo xslFileInfo,bool debug ) {
      return document.Transform( xslFileInfo, debug );
    }
    public static XDocument Transform( this XDocument document, XsltArgumentList args, FileInfo xslFileInfo ) {
      return document.Transform(args, XDocument.Load( xslFileInfo.FullName ),false );
    }
    public static XDocument Transform( this XDocument document, XsltArgumentList args, FileInfo xslFileInfo,bool debug ) {
      return document.Transform( args, XDocument.Load( xslFileInfo.FullName ),debug );
    }
    public static XDocument Transform( this XDocument document, XsltArgumentList args, XDocument xsltDocument ) {
      return document.Transform( args, xsltDocument, false );
    }
    public static XDocument Transform( this XDocument document, XsltArgumentList args, XDocument xsltDocument,bool debug ) {

      XDocument result = null;
      var transformer = new XslCompiledTransform(debug);
      transformer.Load( xsltDocument.CreateReader() );
      var navigator = document.CreateNavigator();
      var sb = new StringBuilder();
      using( var writer = new XmlTextWriter( new StringWriter( sb ) ) ) {
        transformer.Transform( navigator, args, writer );
        result = XDocument.Parse( sb.ToString() );
      }
      return result;
    }
    public static T Deserialize<T>( this XElement element ) {
      XmlSerializer serializer = new XmlSerializer( typeof( T ) );
      var result = ( T )serializer.Deserialize( element.CreateReader() );
      return result;
    }
    static List<ValidationEventArgs> ValidateObject( this XElement element, XmlReaderSettings settings ) {
      List<ValidationEventArgs> result = new List<ValidationEventArgs>();
      settings.ValidationType = ValidationType.Schema;
      settings.ValidationEventHandler += ( sender, args ) => {
        result.Add( args );
      };
      var reader = XmlReader.Create( element.CreateReader(), settings );
      while( reader.Read() ) ;
      return result;
    }
    public static List<ValidationEventArgs> ValidateObject( this XElement element, DirectoryInfo validationDirectory ) {

      XmlReaderSettings settings = new XmlReaderSettings();
      validationDirectory.GetFiles( "*.xsd" ).ToList().ForEach( n => {
        var doc = XDocument.Load( n.FullName );
        var targetNamespace = doc.Elements().First().Attribute( "targetNamespace" ) != null ? doc.Elements().First().Attribute( "targetNamespace" ).Value : String.Empty;
        settings.Schemas.Add( targetNamespace, n.FullName );
      } );
      return element.ValidateObject( settings );
    }
    public static List<ValidationEventArgs> ValidateObject( this XElement element, FileInfo validationFileInfo ) {
      List<ValidationEventArgs> result = new List<ValidationEventArgs>();
      XmlReaderSettings settings = new XmlReaderSettings();

      var doc = XDocument.Load( validationFileInfo.FullName );
      var targetNamespace = doc.Elements().First().Attribute( "targetNamespace" ) != null ? doc.Elements().First().Attribute( "targetNamespace" ).Value : String.Empty;
      settings.Schemas.Add( targetNamespace, validationFileInfo.FullName );
      return element.ValidateObject( settings );
    }
  }
}
