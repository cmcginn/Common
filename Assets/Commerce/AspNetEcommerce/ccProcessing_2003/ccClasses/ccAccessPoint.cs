using System;

using System.Text.RegularExpressions;
using System.Globalization;

using Westwind.InternetTools;


namespace Westwind.WebStore
{
	public class ccAccessPoint : ccProcessing
	{

		public ccAccessPoint()
		{
			this.HttpLink  = "https://secure1.merhcantmanager.com/ccgateway.asp";
		}

		public override bool ValidateCard() 
		{
			if (!base.ValidateCard() )
				return false;

			if (this.OrderAmount < 0) 
			{
				this.ValidatedResult = "FAILED";
				this.ValidatedMessage = "Credits are not supported for AccessPoint's API.";
				this.SetError(this.ValidatedMessage);
				this.LogTransaction();
				return false;
			}

			if (this.Http == null) 
			{
				this.Http = new wwHttp();
				this.Http.Timeout = this.Timeout;
			}

			string CardNo = Regex.Replace(this.CreditCardNumber,@"[ -/._#]","");

			this.Http.AddPostKey("REQUESTTYPE","APPROVALONLY");
			
			if (this.ProcessType == ccProcessTypes.Sale )
				this.Http.AddPostKey("TRANSTYPE","SALE");
			else if (this.ProcessType == ccProcessTypes.PreAuth)
				this.Http.AddPostKey("TRANSTYPE","PREAUTH");

			this.Http.AddPostKey("MERCHANTID",this.MerchantId);
			this.Http.AddPostKey("AMOUNT",this.OrderAmount.ToString(CultureInfo.InstalledUICulture.NumberFormat));
			this.Http.AddPostKey("BNAME",this.Name);
			this.Http.AddPostKey("BCOMPANY",this.Company);
			this.Http.AddPostKey("CCNUMBER",CardNo);
			if (this.SecurityCode.Trim() != "") 
				this.Http.AddPostKey("CVV2",this.SecurityCode.Trim());

			this.Http.AddPostKey("EXPMO",this.CreditCardExpirationMonth.ToString());
			this.Http.AddPostKey("EXPYE",this.CreditCardExpirationYear.ToString());
			this.Http.AddPostKey("BADDRESS1",this.Address);
			this.Http.AddPostKey("BCITY",this.City);
			this.Http.AddPostKey("BSTATE",this.State);
			this.Http.AddPostKey("BZIPCODE",this.Zip);
			this.Http.AddPostKey("BCOUNTRY",this.Country);
			this.Http.AddPostKey("BEMAIL",this.Email);
			this.Http.AddPostKey("BPHONE",this.Phone);
			this.Http.AddPostKey("INVOICENO",this.OrderId);

			if (this.TaxAmount > 0.00M) 
				this.Http.AddPostKey("TAX",this.TaxAmount.ToString(CultureInfo.InstalledUICulture.NumberFormat));

			this.Http.CreateWebRequestObject(this.HttpLink);
			this.Http.WebRequest.Referer=this.ReferringUrl;

			this.RawProcessorResult = this.Http.GetUrl(this.HttpLink);
			if (this.Http.Error) 
			{
				this.ValidatedResult = "FAILED";
				this.ValidatedMessage = this.Http.ErrorMessage;
				this.SetError(this.Http.ErrorMessage);
				this.LogTransaction();
				return false;
			}

			string Approved = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult,"APPROVED");

			if (Approved != "Y") 
			{
				this.ValidatedResult = "DECLINED";
				this.ValidatedMessage = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult,"MSG").Replace("&","");
				if (this.ValidatedMessage == "")
					this.ValidatedMessage = this.RawProcessorResult;
				this.SetError( this.ValidatedMessage );
				this.LogTransaction();
				return false;
			}

			this.ValidatedResult = "APPROVED";

			// *** Make the RawProcessorResult more readable for client application
			this.ValidatedMessage = wwHttpUtils.UrlDecode(this.RawProcessorResult);

			this.LogTransaction();
			return true;
		}
		
	}

}
