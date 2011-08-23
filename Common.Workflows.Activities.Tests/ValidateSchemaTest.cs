using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neovolve.Toolkit.Unity;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.Activities;
using Common.Workflows.Activities;
using System.Xml.Linq;
namespace Common.Workflows.Activities.Tests {
  [TestClass]
  public class ValidateSchemaTest {
    [TestMethod]
    public void TextXslTransform_WhenValidInputs_AssertResultHasElements() {
      //Arrange
      var inputs = new Dictionary<string, object>();
      var activity = new XslTransform();
      var fi = new FileInfo( AppDomain.CurrentDomain.BaseDirectory + "\\orderTransformer.xsl" );
      var element = XElement.Load( AppDomain.CurrentDomain.BaseDirectory + "\\orderMockup.xml" );
      System.Xml.Xsl.XsltArgumentList args = new System.Xml.Xsl.XsltArgumentList();

      inputs.Add( "element", element );
      inputs.Add( "xslFileInfo", fi );
      inputs.Add( "xsltArgumentList", args );
      WorkflowInvoker invoker = new WorkflowInvoker( activity );
      invoker.InvokeCompleted += delegate( object sender, InvokeCompletedEventArgs e ) {
        if( e.Error != null )
          throw new System.Exception( e.Error.Message );
      };
      //Act
      var result = invoker.Invoke( inputs );
      //Assert
      Assert.IsTrue( ( ( XElement )result[ "result" ] ).HasElements );
    }
  }
}
