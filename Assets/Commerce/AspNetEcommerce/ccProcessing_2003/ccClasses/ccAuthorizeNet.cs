using System;

using System.Text.RegularExpressions;
using System.Globalization;

using Westwind.InternetTools;


namespace Westwind.WebStore
{
	
	/// <summary>
	/// This class provides Credit Card processing against 
    /// the Authorize.Net Gateway.
	/// </summary>
	public class ccAuthorizeNet : ccProcessing
	{

		public ccAuthorizeNet()
		{
			this.HttpLink  = "https://secure.authorize.net/gateway/transact.dll";
		}

		/// <summary>
		/// Validates the actual card against Authorize.Net Gateway using the HTTP 
		/// interface.
		/// <seealso>Class ccAuthorizeNet</seealso>
		/// </summary>
		/// <param name=""></param>
		/// <returns>Boolean</returns>
		public override bool ValidateCard() 
		{
			if (!base.ValidateCard() )
				return false;

			if (this.Http == null) 
			{
				this.Http = new wwHttp();
				this.Http.Timeout = this.Timeout;
			}

			string CardNo = Regex.Replace(this.CreditCardNumber,@"[ -/._#]","");

			this.Http.AddPostKey("x_version","3.1");
			this.Http.AddPostKey("x_method","CC");
			this.Http.AddPostKey("x_delim_data","True");
			this.Http.AddPostKey("x_password",this.MerchantPassword);
			this.Http.AddPostKey("x_login",this.MerchantId);

			if (this.UseTestTransaction) 
				this.Http.AddPostKey("x_test_request","True");
			
			if (this.OrderAmount >= 0) 
			{
				if (this.ProcessType == ccProcessTypes.Sale) 
					this.Http.AddPostKey("x_type","AUTH_CAPTURE");
				else if (this.ProcessType == ccProcessTypes.PreAuth) 
					this.Http.AddPostKey("x_type","AUTH_ONLY");
                else if (this.ProcessType == ccProcessTypes.AuthCapture)
                {
                    this.Http.AddPostKey("x_type", "PRIOR_AUTH_CAPTURE");
                    this.Http.AddPostKey("x_trans_ID", this.TransactionId);
                }
                else if (this.ProcessType == ccProcessTypes.Credit)
                    this.Http.AddPostKey("x_type", "CREDIT");

				this.Http.AddPostKey("x_amount",this.OrderAmount.ToString(CultureInfo.InvariantCulture.NumberFormat));
			}
			else 
			{
				this.Http.AddPostKey("x_type","CREDIT");
				this.Http.AddPostKey("x_amount",((int) (-1 * this.OrderAmount)).ToString(CultureInfo.InvariantCulture.NumberFormat));
			}

			if (this.TaxAmount > 0.00M) 
				this.Http.AddPostKey("x_tax",this.TaxAmount.ToString(CultureInfo.InvariantCulture.NumberFormat));


			this.Http.AddPostKey("x_card_num",CardNo);
			this.Http.AddPostKey("x_exp_date",this.CreditCardExpirationMonth.ToString() + "-" +
				                              this.CreditCardExpirationYear.ToString() );
     
			if (this.SecurityCode.Trim() != "") 
				this.Http.AddPostKey("x_card_code",this.SecurityCode.Trim());

			
			this.Http.AddPostKey("x_first_name",this.Firstname);
			this.Http.AddPostKey("x_last_name",this.Lastname);
			this.Http.AddPostKey("x_company",this.Company);
			this.Http.AddPostKey("x_address",this.Address);
			this.Http.AddPostKey("x_city",this.City);
			this.Http.AddPostKey("x_state",this.State);
			this.Http.AddPostKey("x_zip",this.Zip);
			this.Http.AddPostKey("x_country",this.Country);
			
			if (this.Phone.Trim() != "")
				this.Http.AddPostKey("x_phone",this.Phone);

			this.Http.AddPostKey("x_email",this.Email);
	
			this.Http.AddPostKey("x_invoice_num",this.OrderId);
			this.Http.AddPostKey("x_description",this.Comment);

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

			string[] Result = this.RawProcessorResult.Split(new char[1]  {','} );
			if (Result == null)
			{
				this.ValidatedResult = "FAILED";
				this.ValidatedMessage = "Invalid response received from Merchant Server";
				this.SetError(this.ValidatedMessage);
				this.LogTransaction();
				return false;
			}

			// *** REMEMBER: Result Codes are in 0 based array!
            this.TransactionId = Result[6];
            this.AvsResultCode = Result[5];

			if (Result[0] == "3"  )  // Error - Processor communications usually
			{
                // *** Consider an invalid Card a DECLINE
                // *** so we can send back to the customer for display
                if (Result[3].IndexOf("credit card number is invalid") > -1)
                    this.ValidatedResult = "DECLINED";
                else 
                    this.ValidatedResult = "FAILED";    

				this.ValidatedMessage = Result[3];
				this.SetError(this.ValidatedMessage);
			}
            if (Result[0] == "2" || Result[0] == "4")  // Declined
            {
                this.ValidatedResult = "DECLINED";
                this.ValidatedMessage = Result[3];
                if (this.ValidatedMessage == "")
                    this.ValidatedMessage = this.RawProcessorResult;
                this.SetError(this.ValidatedMessage);
            }
            else
            {
                this.ValidatedResult = "APPROVED";

                // *** Make the RawProcessorResult more readable for client application
                this.ValidatedMessage = Result[3];
                this.AuthorizationCode = Result[4];
                this.SetError(null);
            }

			this.LogTransaction();

			return !this.Error;
		}
		
	}

}
