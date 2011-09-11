using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

using Westwind.InternetTools;
using Westwind.Tools;

namespace Westwind.WebStore
{
/// <summary>
/// 
/// </summary>
public class ccPayFlowPro : ccProcessing
{
	/// <summary>
	/// Port on the server (Verisign)
	/// </summary>
    public int HostPort
    {
        get { return _HostPort; }
        set { _HostPort = value; }
    }
    private int _HostPort = 443;


    public string ProxyAddress
    {
        get { return _ProxyAddress; }
        set { _ProxyAddress = value; }
    }
    private string _ProxyAddress = "";

    
    public int ProxyPort
    {
        get { return _ProxyPort; }
        set { _ProxyPort = value; }
    }
    private int _ProxyPort = 0;


    public string ProxyUsername
    {
        get { return _ProxyUsername; }
        set { _ProxyUsername = value; }
    }
    private string _ProxyUsername = "";

    public string ProxyPassword
    {
        get { return _ProxyPassword; }
        set { _ProxyPassword = value; }
    }
    private string _ProxyPassword = "";

	/// <summary>
	/// Sign up partner ID. Required only if you signed up through 
	/// a third party.
	/// </summary>
    public string SignupPartner
    {
        get { return _SignupPartner; }
        set { _SignupPartner = value; }
    }
    private string _SignupPartner = "Verisign";

	/// <summary>
	/// Overridden consistency with API names.
    /// maps to MerchantId
	/// </summary>
    public string UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }
    private string _UserId = "";

    /// <summary>
    /// Internal string value used to hold the values sent to the server
    /// </summary>
    string Parameters = "";

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

		// *** Counter that holds our parameter string to send
		this.Parameters = "";

		// *** Sale and Credit Supported
		decimal TOrderAmount = this.OrderAmount;
		if (this.OrderAmount < 0.00M)
		{
			this.Parameters = "TRXTYPE=C";
			TOrderAmount = this.OrderAmount * -1.0M;
		}
		else
			this.Parameters = "TRXTYPE=S";

		string CardNo = Regex.Replace(this.CreditCardNumber,@"[ -/._#]","");
		
		string TUserId = this.UserId;

		if (TUserId == "")
			TUserId = this.MerchantId;

		string ExpDate = string.Format("{0:##}",this.CreditCardExpirationMonth) ;
		ExpDate +=  this.CreditCardExpirationYear;

		this.Parameters += "&TENDER=C" +
			              "&ACCT=" + CardNo +
						  "&VENDOR=" + this.MerchantId +
						  "&USER=" + TUserId +
						  "&PWD=" + this.MerchantPassword +
						  "&PARTNER=" + this.SignupPartner +
						  "&AMT=" + TOrderAmount.ToString(CultureInfo.InvariantCulture.NumberFormat) + 
						  "&EXPDATE=" + ExpDate +
						  "&STREET=" + this.Address +
						  "&CITY=" + this.City +
						  "&STATE=" + this.State +
						  "&ZIP=" + this.Zip +
						  "&EMAIL=" + this.Email +
						  "&COMMENT1=" + this.Comment;

		
		if (this.TaxAmount > 0.00M)
            this.Parameters += "&TAXAMT=" + this.TaxAmount.ToString(CultureInfo.InvariantCulture.NumberFormat);

		if (this.SecurityCode != "")
			this.Parameters += "&CVV2=" + this.SecurityCode;


        // ***  Save our raw input string for debugging
        this.RawProcessorRequest = this.Parameters;

		// *** connects to Verisign COM object to handle transaction
		System.Type typPayFlow = System.Type.GetTypeFromProgID( "PFProCOMControl.PFProCOMControl.1" );
		object PayFlow = Activator.CreateInstance( typPayFlow );
		
		this.RawProcessorResult = "";

		try 
		{
            // *** Use Reflection and Late binding to call COM object
            // *** to avoid creating Interop assembly
			int context = (int) wwUtils.CallMethod(PayFlow,"CreateContext",this.HttpLink,this.HostPort,this.Timeout,
								   this.ProxyAddress,this.ProxyPort,this.ProxyUsername, this.ProxyPassword);
			
            this.RawProcessorResult = (string) wwUtils.CallMethod(PayFlow,"SubmitTransaction",context,Parameters,Parameters.Length);

			wwUtils.CallMethod(PayFlow, "DestroyContext",context);
		}
		catch(Exception ex)
		{
			this.ValidatedResult = "FAILED";
			this.ValidatedMessage = ex.Message;
			this.LogTransaction();
			return false;
		}

		string ResultValue = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult,"RESULT");

        this.TransactionId = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult, "PNREF");


		// *** 0 means success 
		if (ResultValue == "0") 
		{
			this.ValidatedResult = "APPROVED";
            this.AuthorizationCode = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult, "AUTHCODE");
            this.LogTransaction();
			return true;
		}
			// *** Empty response means we have an unknown failure
		else if (ResultValue == "")
		{
			this.ValidatedMessage = "Unknown Error";
			this.ValidatedResult = "FAILED";
		}
			// *** Negative number means communication failure
		else if (ResultValue.StartsWith("-") )
		{
			this.ValidatedResult = "FAILED";
			this.ValidatedMessage = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult,"RESPMSG");
			
		}
			// *** POSITIVE number means we have a DECLINE 
		else 
		{
		     this.ValidatedResult = "DECLINED";
     		 this.ValidatedMessage = wwHttpUtils.GetUrlEncodedKey(this.RawProcessorResult,"RESPMSG");
		}

		this.Error = true;
		this.LogTransaction();
		return false;
	}

}
}
