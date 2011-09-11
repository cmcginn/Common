/*

Set the EnableLinkPoint Flag either here or via compiler Constant switch to enable this interface. 

By default this class is disabled (not included) in order to avoid pulling in the required
References to referenced Assemblies for the LinkPoint API. When disabled this class won't
instantiate - it will throw and Exception in the constructor.

To Enable this class:

*  Make sure to copy LinkPointTransaction.dll and lpSSL.dll from the LinkPoint SDK 
   into your /bin or executable directory
*  Make sure you installed the OpenSSL DLL per LinkPoint (see Web Store ccLinkPoint help for details)
*  Add a reference to the project for LinkPointTransaction.dll
 
*/
//#define EnableLinkPoint  

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

using Westwind.InternetTools;
using Westwind.Tools;

#if EnableLinkPoint
using LinkPointTransaction;
#endif

namespace Westwind.WebStore
{
/// <summary>
/// The ccLinkPoint class provides an ccProcessing interface to the 
/// LinkPoint 6.0 interface.
/// </summary>
public class ccLinkPoint : ccProcessing
{
	/// <summary>
	/// The Port used by the LinkPoint API to communicate with the server.
	/// This port will be provided to you by LinkPoint. Note that you
	/// can also provide the port as part of the HTTPLink domainname.
	/// </summary>
	public int HostPort = 1129;

	public ccLinkPoint()
	{
		this.HttpLink = "secure.linkpt.net:1129";
	}

	#if EnableLinkPoint

	/// <summary>
	/// Validates the credit card. Supported transactions include only Sale or Credit.
	/// Credits should have a negative sales amount.
	/// </summary>
	/// <returns></returns>
	public override bool ValidateCard() 
	{
		if (!base.ValidateCard()) 
			return false;
		
		this.Error = false;
		this.ErrorMessage = "";

		// *** Parse the HttpLink for domain + port number
		int At = this.HttpLink.IndexOf(":");
		if (At > 0) 
		{
			string t = this.HttpLink.Substring(At+1);
			this.HostPort = int.Parse( t );
			this.HttpLink = this.HttpLink.Substring(0,At);
		}

		// create order
		LPOrderPart order = LPOrderFactory.createOrderPart("order");

		// create a part we will use to build the order
		LPOrderPart op = LPOrderFactory.createOrderPart();
			
		// Figure out what type of order we are processing
		if (this.OrderAmount < 0 ) 
		{
			op.put("ordertype","CREDIT");
			this.OrderAmount = this.OrderAmount * -1;
		}
		else if (this.ProcessType == ccProcessTypes.Credit )
			op.put("ordertype","CREDIT");
		else if (this.ProcessType == ccProcessTypes.Sale )
			op.put("ordertype","SALE");
		else if (this.ProcessType == ccProcessTypes.PreAuth )
			op.put("ordertype","PREAUTH");
					
		// add 'orderoptions to order
		order.addPart("orderoptions", op ); 

		// Build 'merchantinfo'
		op.clear();
		op.put("configfile",this.MerchantId);

		// add 'merchantinfo to order
		order.addPart("merchantinfo", op );

		// Build 'billing'
		// Required for AVS. If not provided, 
		// transactions will downgrade.
		op.clear();
		op.put("name",this.Name);
		op.put("address1",this.Address);
		op.put("city",this.City);
		op.put("state",this.State);
		op.put("zip",this.Zip);
		op.put("country",this.Country);
		op.put("email",this.Email);
		op.put("phone",this.Phone);
		
		int  AddrNum = 0;
		try 
		{
			string T = this.Address.Substring( 0 , this.Address.IndexOf(" ") ).Trim();
			AddrNum = int.Parse(T);
		}
		catch { ; }
		
		if (AddrNum != 0)  
		{
			op.put("addrnum",  AddrNum.ToString( System.Globalization.CultureInfo.InvariantCulture.NumberFormat) );
		}
		order.addPart("billing", op );

		// Build 'creditcard'
		op.clear();
		op.put("cardnumber",this.CreditCardNumber );
		op.put("cardexpmonth",this.CreditCardExpirationMonth);
		op.put("cardexpyear",this.CreditCardExpirationYear);

		order.addPart("creditcard", op );

		// Build 'payment'
		op.clear();
		op.put("chargetotal",this.OrderAmount.ToString( System.Globalization.CultureInfo.InvariantCulture.NumberFormat ) );

		order.addPart("payment", op );	

		// *** Trasnaction Details
		op.clear();
		if (this.OrderId != null && this.OrderId != "" )
			op.put("oid",this.OrderId );

		order.addPart("transactiondetails",op);

		if (this.Comment != "" )
		{
			// *** Notes
			op.clear();
			op.put("comments",this.Comment );
			order.addPart("notes",op);
		}

		// create transaction object	
		LinkPointTxn LPTxn = new LinkPointTxn();

		// get outgoing XML from the 'order' object
		this.RawProcessorRequest = order.toXML();

		// Call LPTxn
		try 
		{
			this.RawProcessorResult = LPTxn.send(this.CertificatePath,this.HttpLink,this.HostPort, this.RawProcessorRequest );
		}
		catch(Exception ex)
		{
			if (this.RawProcessorResult != "") 
			{
				string Msg = wwUtils.ExtractString(this.RawProcessorResult,"<r_error>","</r_error>") ;
				if (Msg == "")
					this.SetError(ex.Message);
				else
					this.SetError(Msg);
			}
			else 
			{	
				this.SetError(ex.Message);
			}

			this.ValidatedMessage = this.ErrorMessage;
			this.ValidatedResult = "FAILED";
			return false;
		}
		
		this.ValidatedResult = wwUtils.ExtractString(this.RawProcessorResult,"<r_approved>","</r_approved>");
		if (this.ValidatedResult == "")
			this.ValidatedResult = "FAILED";
		this.ValidatedMessage = wwUtils.ExtractString(this.RawProcessorResult,"<r_message>","</r_message>");

		
		if (this.ValidatedResult == "APPROVED") 
		{
			this.SetError(null);
		}
		else 
		{
			this.SetError( wwUtils.ExtractString(this.RawProcessorResult,"<r_error>","</r_error>") );
			this.ValidatedMessage = this.ErrorMessage;
		}
		
		this.LogTransaction();

		return !this.Error;
	}
	#else
	public override bool ValidateCard() 
	{
		throw new Exception("ccLinkPoint Class is not enabled. Set the EnableLinkPoint #define in the source file.");
	}
	#endif
}
}

