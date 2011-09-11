using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

using Westwind.InternetTools;

namespace Westwind.WebStore
{
	/// <summary>
	/// The ccProcessing class provides a base class for provider processing of 
	/// requests. The class aims at providing a simple, common interface to Credit 
	/// Card Processing so it's easy to swap providers.
	/// 
	/// This class acts as an abstract class that requires subclassing and use of a
	/// specific provider implementation class. However, because the functionality is 
	/// so common amongst providers a single codebase can usually accomodate all requests.
	/// </summary>
	public abstract class ccProcessing
    {

        #region Logon Properties
        /// <summary>
        /// The merchant Id or store name or other mechanism used to identify your account.
        /// </summary>
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }
        private string _MerchantId = "";

        /// <summary>
        /// The merchant password for your merchant account. Not used in most cases.
        /// </summary>
        public string MerchantPassword
        {
            get { return _MerchantPassword; }
            set { _MerchantPassword = value; }
        }
        private string _MerchantPassword = "";

#endregion

        #region Processor Configuration
        /// <summary>
        /// Reference to an wwHttp object. You can preseed this object after instantiation to 
        /// allow setting custom HTTP settings prior to calling ValidateCard().
        /// 
        /// Used only with providers that use direct POST operations to a Web Server
        /// including Authorize.NET, AccessPoint and BluePay
        /// </summary>
        public wwHttp Http
        {
            get { return _Http; }
            set { _Http = value; }
        }
        private wwHttp _Http = null;

        
        /// <summary>
        /// The link to hit on the server. Depending on the interface this can be a 
        /// URL or domainname or domainname:Port combination.
        /// 
        /// Applies only to providers that use HTTP POST operations directly.
        /// </summary>
        public string HttpLink
        {
            get { return _HttpLink; }
            set { _HttpLink = value; }
        }
        private string _HttpLink = "";


        /// <summary>
        /// Timeout in seconds for the connection against the remote processor
        /// </summary>
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }
        private int _Timeout = 50;

        /// <summary>
        /// Optional flag that determines whether to send a test transaction
        /// Not supported for AccessPoint
        /// </summary>
        public bool UseTestTransaction
        {
            get { return _UseTestTransaction; }
            set { _UseTestTransaction = value; }
        }
        private bool _UseTestTransaction = false;

        /// <summary>
        /// Determines whether a Mod10Check is performed before sending the 
        /// credit card to the processor. Turn this off for testing so you
        /// can at least get to the provider.
        /// </summary>
        public bool UseMod10Check
        {
            get { return _UseMod10Check; }
            set { _UseMod10Check = value; }
        }
        private bool _UseMod10Check = true;

        /// <summary>
        /// Referring Url used with certain providers
        /// </summary>
        public string ReferringUrl
        {
            get { return _ReferringUrl; }
            set { _ReferringUrl = value; }
        }
        private string _ReferringUrl = "";


#endregion
            

        #region Credit Card Information
        /// <summary>
		/// The credit card number. Number can contain spaces and other markup 
		/// characters which are stripped for processing later.
		/// </summary>
        public string CreditCardNumber
        {
            get { return _CreditCardNumber; }
            set { _CreditCardNumber = value; }
        }
        private string _CreditCardNumber = "";

        /// <summary>
        /// The 3 or 4 letter digit that is on the back of the card
        /// </summary>
        public string SecurityCode
        {
            get { return _SecurityCode; }
            set { _SecurityCode = value; }
        }
        private string _SecurityCode = "";

		/// <summary>
		/// Full expiration date in the format 01/2003
		/// </summary>
		public string CreditCardExpiration 
		{
			get { return this._CreditCardExpiration; }
			set	
			{
				string Exp = value.Trim();
				string[] Split = Exp.Split("/-.\\,".ToCharArray(),2);
				this.CreditCardExpirationMonth = Split[0].PadLeft(2,'0');
				this.CreditCardExpirationYear = Split[1];
				if (this.CreditCardExpirationYear.Length == 4)
					this.CreditCardExpirationYear = this.CreditCardExpirationYear.Substring(2,2);
				this._CreditCardExpiration = Exp;
			}
		}
		string _CreditCardExpiration = "";

		/// <summary>
		/// Credit Card Expiration Month as a string (2 digits ie. 08)
		/// </summary>
        public string CreditCardExpirationMonth
        {
            get { return _CreditCardExpirationMonth; }
            set { _CreditCardExpirationMonth = value; }
        }
        private string _CreditCardExpirationMonth = "";

		
		/// <summary>
		/// Credit Card Expiration Year as a 4 digit string
		/// </summary>
        public string CreditCardExpirationYear
        {
            get { return _CreditCardExpirationYear; }
            set { _CreditCardExpirationYear = value; }
        }
        private string _CreditCardExpirationYear = "";


        /// <summary>
        /// Determines what type of transaction is being processed (Sale, Credit, PreAuth)
        /// </summary>
        public ccProcessTypes ProcessType
        {
            get { return _ProcessType; }
            set { _ProcessType = value; }
        }
        private ccProcessTypes _ProcessType = ccProcessTypes.Sale;


        #endregion

        #region Order Information
        /// <summary>
		/// The amount of the order.
		/// </summary>
        public decimal OrderAmount
        {
            get { return _OrderAmount; }
            set { _OrderAmount = value; }
        }
        private decimal _OrderAmount = 0.00M;

		/// <summary>
		/// The amount of Tax for this transaction
		/// </summary>
        public decimal TaxAmount
        {
            get { return _TaxAmount; }
            set { _TaxAmount = value; }
        }
        private decimal _TaxAmount = -1.00M;

        /// <summary>
        /// The Order Id as a string. This is mainly for reference but should be unique.
        /// </summary>
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        private string _OrderId = "";

        /// <summary>
        /// Order Comment. Usually this comment shows up on the CC bill.
        /// </summary>
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        private string _Comment = "";

#endregion

        #region Customer/Billing Information

        /// <summary>
		/// First name and last name on the card
		/// </summary>
		public string Name 
		{
			get 
			{ 
				if (_Name == "") 
					return ( (string)(Firstname + " " + Lastname)).Trim();

				return _Name;
			}
			set { _Name = value; }
		}
		string _Name = "";

		/// <summary>
		/// First Name of customer's name on the card. Can be used in lieu of Name 
		/// property. If Firstname and Lastname are used they get combined into a name 
		/// IF Name is blank.
		/// <seealso>Class ccProcessing</seealso>
		/// </summary>
        public string Firstname
        {
            get { return _Firstname; }
            set { _Firstname = value; }
        }
        private string _Firstname = "";

		

		/// <summary>
		/// Last Name of customer's name on the card. Can be used in lieu of Name 
		/// property. If Firstname and Lastname are used they get combined into a name 
		/// IF Name is blank.
		/// 
		/// AuthorizeNet requires both first and last names, while the other providers 
		/// only use a single Name property.
		/// <seealso>Class ccProcessing</seealso>
		/// </summary>
        public string Lastname
        {
            get { return _Lastname; }
            set { _Lastname = value; }
        }
        private string _Lastname = "";

		
		/// <summary>
		/// Billing Company
		/// </summary>
        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }
        private string _Company = "";

		
		/// <summary>
		/// Billing Street Address.
		/// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Address = "";

		/// <summary>
		/// Billing City
		/// </summary>
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _City = "";

		/// <summary>
		/// Billing State (2 letter code or empty for foreign)
		/// </summary>
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _State = "";

		/// <summary>
		/// Postal or Zip code
		/// </summary>
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Zip = "";
		
		/// <summary>
		/// Two letter Country Id -  US, DE, CH etc.
		/// </summary>
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private string _Country = "";

		/// <summary>
		/// Billing Phone Number 
		/// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _Phone = "";

		/// <summary>
		/// Email address
		/// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Email = "";

        #endregion


        #region Result Properties
        /// <summary>
		/// The raw response from the Credit Card Processor Server.
		/// 
		/// You can use this result to manually parse out error codes
		/// and messages beyond the default parsing done by this class.
		/// </summary>
        public string RawProcessorResult
        {
            get { return _RawProcessorResult; }
            set { _RawProcessorResult = value; }
        }
        private string _RawProcessorResult = "";

		/// <summary>
		/// This is a string that contains the format that's sent to the
		/// processor. Not used with all providers, but this property can
		/// be used for debugging and seeing what exactly gets sent to the
		/// server.
		/// </summary>
        public string RawProcessorRequest
        {
            get { return _RawProcessorRequest; }
            set { _RawProcessorRequest = value; }
        }
        private string _RawProcessorRequest = "";

		/// <summary>
		/// The parsed short form response. APPROVED, DECLINED, FAILED, FRAUD
		/// </summary>
        public string ValidatedResult
        {
            get { return _ValidatedResult; }
            set { _ValidatedResult = value; }
        }
        private string _ValidatedResult = "";

		/// <summary>
		/// The parsed error message from the server if result is not APPROVED
		/// This message generally is a string regarding the failure like 'Invalid Card'
		/// 'AVS Error' etc. This info may or may not be appropriate for your customers
		/// to see - that's up to you.
		/// </summary>
        public string ValidatedMessage
        {
            get { return _ValidatedMessage; }
            set { _ValidatedMessage = value; }
        }
        private string _ValidatedMessage = "";


        /// <summary>
        /// The Transaction ID returned from the server. Use to match
        /// transactions against the gateway for reporting.
        /// </summary>
        public string TransactionId
        {       
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        private string _TransactionId = "";


        /// <summary>
        /// Authorization Code returned for Approved transactions from the gateway
        /// </summary>
        public string AuthorizationCode
        {
            get { return _AuthorizationCode; }
            set { _AuthorizationCode = value; }
        }
        private string _AuthorizationCode = "";

        /// <summary>
        /// The AVS Result code from the gateway if available
        /// </summary>
        public string AvsResultCode
        {
            get { return _AvsResultCode; }
            set { _AvsResultCode = value; }
        }
        private string _AvsResultCode = "";



		/// <summary>
		/// Used for Linkpoint only. Specifies the path to the certificate file
		/// </summary>
		public string CertificatePath = "";


        #endregion

        #region Errors and Debugging

        /// <summary>
		/// Optional path to the log file used to write out request results.
		/// If this filename is blank no logging occurs. The filename specified
		/// here needs to be a fully qualified operating system path and the
		/// application has to be able to write to this path.
		/// </summary>
        public string LogFile
        {
            get { return _LogFile; }
            set { _LogFile = value; }
        }
        private string _LogFile = "";

		
            
        /// <summary>
		/// Error flag set after a call to ValidateCard if an error occurs.
		/// </summary>
        public bool Error
        {
            get { return _Error; }
            set { _Error = value; }
        }
        private bool _Error = false;

		/// <summary>
		/// Error message if error flag is set or negative result is returned.
		/// Generally this value will contain the value of this.ValidatedResult
		/// for processor failures or more general API/HTTP failure messages.
		/// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        private string _ErrorMessage = "";


        #endregion

        /// <summary>
		/// Base ValidateCard method that provides the core CreditCard checking. Should 
		/// always be called at the beginning of the subclassed overridden method.
		/// </summary>
		/// <returns>bool</returns>
		public virtual bool ValidateCard()
		{
			if (this.UseMod10Check && !Mod10Check(this.CreditCardNumber) ) 
			{
				this.ErrorMessage = "Invalid Credit Card Number";
				this.ValidatedMessage = this.ErrorMessage;
				this.ValidatedResult = "DECLINED";
				this.LogTransaction();
				return false;
			}

			return true;
		}



		/// <summary>
		/// Logs the information of the current request into the log file specified.
		/// If the log file is empty no logging occurs.
		/// </summary>
		/// <param name="lcLogString"></param>
		protected virtual void LogTransaction()
		{
			if (this.LogFile != "") 
			{
				string CardNo = this.CreditCardNumber;
				if (this.CreditCardNumber.Length > 10)
					CardNo = CardNo.Substring(0,10);

				byte[] binLogString = Encoding.Default.GetBytes(DateTime.Now.ToString() +  " - " + 
					                                            this.Name + " - " + CardNo + " - " + 
																this.ValidatedResult + " - " + 
					                                            this.ValidatedMessage + " - " + 
																this.OrderAmount.ToString(CultureInfo.InstalledUICulture.NumberFormat) + "\r\n");
				
				lock(this) 
				{
					try 
					{
						FileStream loFile = new FileStream(LogFile,FileMode.OpenOrCreate,FileAccess.Write,FileShare.Write);
						loFile.Seek(0,SeekOrigin.End);
						loFile.Write(binLogString,0,binLogString.Length);
						loFile.Close();
					}
					catch { ; }
				}
			}
		}


        /// <summary>
        /// Returns a string for a single AVS code value
        /// Supported codes:
        /// ANSUWXYZER_
        /// </summary>
        /// <param name="AvsCode"></param>
        /// <returns></returns>
        public string AvsCodeToString(string AvsCode)
        {
            if (AvsCode == "A")
                return "Street Address Matched, Zip not Matched";
            if (AvsCode == "N")
                return "No AVS Match";
            if (AvsCode == "U")
                return "AVS not supported for this card type";
            if (AvsCode == "W")
                return "Zip 9 matched, no street match";
            if (AvsCode == "X")
                return "Zip 9 matched, street matched";
            if (AvsCode == "Y")
                return "Zip 5 matched, street matched";
            if (AvsCode == "Z")
                return "Zip 5 matched, street not matched";
            if (AvsCode == "E")
                return "Not eligible for AVS";
            if (AvsCode == "R")
                return "System unavailable";
            if (AvsCode == "_")
                return "AVS supported on this network or transaction type";

            return "";
        }

		/// <summary>
		/// Determines whether given string passes standard Mod10 check.
		/// </summary>
		/// <param name="validString">String to be validated</param>
		/// <returns>True if valid, otherwise false</returns>
		public static bool Mod10Check(string StringToValidate)
		{
			string trimString = StringToValidate.Trim();
			char lastChar = trimString[trimString.Length - 1];

			char checkSumChar = Mod10Worker(trimString.Substring(0, trimString.Length - 1), false);
			return (lastChar == checkSumChar);
		}

		/// <summary>
		/// Worker Mod10 check method that figures out the Mod10 checksum value for a string
		/// </summary>
		/// <param name="chkString">String to be validated</param>
		/// <param name="startLeft">Specifies if check starts from left</param>
		/// <returns>Checksum character</returns>
		private static char Mod10Worker(string chkString, bool startLeft)
		{
			// Remove any non-alphanumeric characters (The list can be expanded by adding characters
			// to RegEx.Replace pattern parameter's character set)
			string chkValid = chkString;
			chkValid = Regex.Replace(chkValid, @"[\s-\.]*", "").ToUpper();
			
			// Calculate the MOD 10 check digit
			int Mod10 = 0;
			int Digit;
			char curChar;

			for (int Pos = 0; Pos <= chkValid.Length - 1; Pos++)
			{
				// Take each char starting from the right unless startLeft is true
				if (startLeft)
				{
					Digit = Pos;
				}
				else
				{
					Digit = chkValid.Length - Pos - 1;
				}
			
				curChar = chkValid[Digit];
					
				// If the character is a digit, take its numeric value.
				if (System.Char.IsDigit(curChar))
				{
					Digit = (int)System.Char.GetNumericValue(curChar);
				}
				else
				{
					// Otherwise, take the base16 value of the letter.
					// NOTE: The .... is a place holder to force the / to equal 15.
					// The USPS does not assign a value for a period. Periods were removed from
					// string at beginning of the function
					string base16String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ..../";
					int foundPos = base16String.IndexOf(curChar) + 1;
					Digit = (foundPos >= 0 ? foundPos % 16 : 0);							
				}
					
				// Multiply by 2 if the position is odd
				Digit = Digit * ((Pos + 1) % 2 == 0 ? 1 : 2);

				// If the result is larger than 10, the two digits have to be added. This can be done 
				// by adding the results of the integer division by 10 and then mod by 10.
				Digit = (Digit / 10) + (Digit % 10);
					
				// If the result is 10 (which occurs when N is an odd position), add the two digits together again
				// (this becomes 1).
				Digit = (Digit == 10 ? 1 : Digit);
					
				// Sum all the digits
				Mod10 = Mod10 + Digit;
			}

			// Subtract the MOD 10 from 10
			Mod10 = 10 - (Mod10 % 10);

			// If the result is 10, then the check digit is 0.
			// Else, it is the result. Return it as a character
			Mod10 = (Mod10 == 10 ? 0 : Mod10);
				
			return Mod10.ToString()[0];
		}


		/// <summary>
		/// Error setting method that sets the Error flag and message.
		/// </summary>
		/// <param name="lcErrorMessage"></param>
		protected void SetError(string ErrorMessage)
		{
			if (ErrorMessage == null || ErrorMessage.Length == 0) 
			{
				this.ErrorMessage = "";
				this.Error = false;
				return;
			}

			this.ErrorMessage = ErrorMessage;
			this.Error = true;
		}
	}

	/// <summary>
	/// Credit Card Processors available
	/// </summary>
	public enum ccProcessors 
	{
		AuthorizeNet,
		AccessPoint,
		PayFlowPro,
		LinkPoint,
        BluePay
	}

	public enum ccProcessTypes
	{
		Sale,
		Credit,
		PreAuth,
        AuthCapture
	}

}
