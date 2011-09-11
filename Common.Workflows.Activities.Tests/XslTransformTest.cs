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
  public class XslTransformTest {
    [TestMethod]
    public void TextValidateSchema_WhenElementInvalid_AssertValidationResultArgsListCount() {
      //Arrange
      var inputs = new Dictionary<string, object>();
      var activity = new ValidateSchema();
      var validationSchemaFile = new FileInfo( AppDomain.CurrentDomain.BaseDirectory + "\\ANetApi.xsd" );
      var element = XElement.Load( AppDomain.CurrentDomain.BaseDirectory + "\\invalidOrder.xml" );
      
      
      inputs.Add( "validationTarget", element );
      inputs.Add( "validationSchemaFile", validationSchemaFile );
      
      WorkflowInvoker invoker = new WorkflowInvoker( activity );
      invoker.InvokeCompleted += delegate( object sender, InvokeCompletedEventArgs e ) {
        if( e.Error != null )
          throw new System.Exception( e.Error.Message );
      };
      //Act
      var result = invoker.Invoke(inputs);
      //Assert
      Assert.IsTrue( ( ( System.Collections.Generic.List<System.Xml.Schema.ValidationEventArgs> )result[ "result" ] ).Count>0);
    }
  }
}
