using System;

using Westwind.WebStore;

namespace PayPalIntegration
{

	public class App 
	{

		public static WebConfiguration Configuration = new WebConfiguration();

	}
	/// <summary>
	/// Configuration class used for the demos
	/// </summary>
	public class WebConfiguration
	{
		public WebConfiguration()
		{
	
		}
		public  string AccountEmail = "rstrahl@west-wind.com";
		public  string PayPalUrl = "https://www.paypal.com/cgi-bin/webscr?";


		/// <summary>
		/// CC Processor options
		/// AccessPoint - only one supported at this time
		/// </summary>
		public  ccProcessors CCProcessor = ccProcessors.AuthorizeNet;

		/// <summary>
		/// Determines whether Credit Cards are processed online.
		/// Requires that the other cc Keys are set.
		/// </summary>
		public bool ccProcessCardsOnline = false;

		/// <summary>
		/// The MerchantId or Store ID
		/// </summary>
		public  string CCMerchantId = "";

		/// <summary>
		/// Merchant Password if one is required
		/// </summary>
		public string CCMerchantPassword = "";

		/// <summary>
		/// Determines how orders are processed either as a Sale or Pre-Auth
		/// </summary>
		public ccProcessTypes CCProcessType = ccProcessTypes.Sale;

		/// <summary>
		/// HTTP Url to do remote processing. Depends on the gateway. See
		/// ccProcessing.chm help file for more details on urls and gateway 
		/// configuration settings.
		/// </summary>
		public  string CCHostUrl = "https://secure.authorize.net/gateway/transact.dll";

		/// <summary>
		/// Refering URL - some services require that this is provided.
		/// </summary>
		public  string CCReferingOrderUrl = "";

		/// <summary>
		/// The path to a Certificate file for credit card processing.
		/// </summary>
		public string CCCertificatePath = "";

		/// <summary>
		/// Url used to access PayPal
		/// </summary>
		public string CCPayPalUrl = "https://www.paypal.com/cgi-bin/webscr?";

		/// <summary>
		/// The Email Address for the account to distribute money to.
		/// </summary>
		public string CCPayPalEmail = "rstrahl@west-wind.com";

		/// <summary>
		/// Determines wheter PayPal transactions can be auto-confirmed. 
		/// Useful to first check out PayPal transactions for fraud.
		/// </summary>
		public bool CCPayPalAllowAutoConfirmation = true;

		/// <summary>
		/// The amount of time in seconds that we wait for completion of processing
		/// </summary>
		public int CCConnectionTimeout = 60;

		/// <summary>
		/// Location of the Credit Card Log File. If empty no logging occurs.
		/// Requires a fully qualified OS path.
		/// </summary>
		public string CCLogFile = "";

//		public static string AccountEmail = "PayPalEmailAccount@YourCompany.com";
//		public static string PayPalUrl = "https://www.sandbox.paypal.com/us/cgi-bin/webscr?";
		//"https://www.paypal.com/cgi-bin/webscr?";
		//"https://www.sandbox.paypal.com/us/cgi-bin/webscr?";
	}
}
