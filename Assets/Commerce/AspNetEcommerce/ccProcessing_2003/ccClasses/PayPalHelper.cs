using System;
using System.IO;
using System.Text;
using System.Web;

using Westwind.InternetTools;

namespace PayPalIntegration
{
	/// <summary>
	/// Summary description for PayPalUrl.
	/// </summary>
	public class PayPalHelper
	{
		public string LogoUrl = "";
		public string AccountEmail = "";
		public string BuyerEmail = "";
		public string SuccessUrl = "";
		public string CancelUrl = "";
		public string ItemName = "";
		public decimal Amount = 0.00M;
		public string InvoiceNo = "";

		
		public string PayPalBaseUrl = "https://www.paypal.com/cgi-bin/webscr?";
//"https://www.paypal.com/cgi-bin/webscr?";
//"https://www.sandbox.paypal.com/us/cgi-bin/webscr?";


		public string LastResponse = "";

		public PayPalHelper()
		{

		}

		public string GetSubmitUrl()
		{
			StringBuilder	url	= new StringBuilder();

			url.Append( this.PayPalBaseUrl + "cmd=_xclick&business="+
				HttpUtility.UrlEncode( AccountEmail ) );

			if( BuyerEmail != null && BuyerEmail != "" )
				url.AppendFormat( "&email={0}", HttpUtility.UrlEncode( BuyerEmail ) );

			if (Amount != 0.00M) 
				url.AppendFormat("&amount={0:f2}", Amount);

			if( LogoUrl != null && LogoUrl != "" )
				url.AppendFormat( "&image_url={0}", HttpUtility.UrlEncode( LogoUrl ) );

			if( ItemName != null && ItemName != "" )
				url.AppendFormat( "&item_name={0}", HttpUtility.UrlEncode( ItemName ) );

			if( InvoiceNo  != null && InvoiceNo != "" )
				url.AppendFormat( "&invoice={0}", HttpUtility.UrlEncode( InvoiceNo ) );

			if( SuccessUrl != null && SuccessUrl != "" )
				url.AppendFormat( "&return={0}", HttpUtility.UrlEncode( SuccessUrl ) );

			if( CancelUrl != null && CancelUrl != "" )
				url.AppendFormat( "&cancel_return={0}", HttpUtility.UrlEncode( CancelUrl ) );

			return url.ToString();
		}

		/// <summary>
		/// Posts all form variables received back to PayPal. This method is used on 
		/// is used for Payment verification from the 
		/// </summary>
		/// <returns>Empty string on success otherwise the full message from the server</returns>
		public bool IPNPostDataToPayPal(string PayPalUrl,string PayPalEmail) 
		{			
			HttpRequest Request = HttpContext.Current.Request;
			this.LastResponse = "";

			// *** Make sure our payment goes back to our own account
			string Email = Request.Form["receiver_email"];
			if (Email == null || Email.Trim().ToLower() != PayPalEmail.ToLower()) 
			{
				this.LastResponse = "Invalid receiver email";
				return false;
			}
	
			wwHttp Http = new wwHttp();
			Http.AddPostKey("cmd","_notify-validate");

			foreach (string postKey in Request.Form)
				Http.AddPostKey(postKey,Request.Form[postKey]);
    
			// *** Retrieve the HTTP result to a string
			this.LastResponse = Http.GetUrl(PayPalUrl);
    
			if (this.LastResponse ==  "VERIFIED" )
				return true;

			return false;
		}

	}
}
