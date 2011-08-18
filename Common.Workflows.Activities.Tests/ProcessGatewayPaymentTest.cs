using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.Activities;
using Common.Workflows.Activities;
namespace Common.Workflows.Activities.Tests {
  [TestClass]
  public class ProcessGatewayPaymentTest {
    [TestMethod]
    [Description("Assures container items created correctly")]
    public void TestCreation() {
      //Arrange
      var inputs = new Dictionary<string, object>();
      var activity = new ProcessGatewayPayment();
      inputs.Add("validationDirectoryInfo",new System.IO.DirectoryInfo( "c:\\Temp" ));
     
      WorkflowInvoker invoker = new WorkflowInvoker( activity );
      invoker.InvokeCompleted += delegate( object sender, InvokeCompletedEventArgs e ) {
        if( e.Error != null )
          throw new System.Exception( e.Error.Message );
      };
      //Act
      var result = invoker.Invoke( inputs );
      //Assert
     
    }
  }
}
