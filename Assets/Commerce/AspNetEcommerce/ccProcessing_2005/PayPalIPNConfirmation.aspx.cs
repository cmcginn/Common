/*

Remember in order to test IPN with the debugger you will need to test
the application on a publicly accessible IP address so PayPal can find
your machine.

You need to configure PayPal by:

* Login to PayPal
* Go to your Profile
* Instant Payment Notification
* Set the checkbox and set the URL to your local server/ip for this page
* When you go live remember to change this URL to your live site
* Only one IPN address per PayPal account is allowed.


Rick Strahl
West Wind Technologies
http://www.west-wind.com/

Article: 
http://www.west-wind.com/presentations/paypalintegration/paypalintegration.asp


*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Westwind.InternetTools;

namespace PayPalIntegration
{
	/// <summary>
	/// Paypal IPN Confirmation page that get's 'called' back by PayPal
	/// after successful payment has been made. This page then emails
	/// an order confirmation to the customer and is handled in 
	/// the same was as an ordinary processed order.
	/// </summary>
	public partial class PayPalIPNConfirmation : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			PayPalHelper PayPal = new PayPalHelper();
			
			// *** Send the response data back to PayPal for confirmation
			bool Result = PayPal.IPNPostDataToPayPal(App.Configuration.PayPalUrl,
				                                     App.Configuration.AccountEmail);
			if (Result) 
			{
				// *** Success! Finalize your order, send email etc.
				
			}
			else 
			{
				try 
				{	
					// *** Failure: Log the failure...
//					Westwind.Tools.WebRequestLog.LogCustomMessage(App.Configuration.ConnectionString, 
//						                                          Westwind.Tools.WebRequestLogMessageTypes.ApplicationMessage,PayPal.LastResponse);
				}
				catch {;}
			}
		}
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
