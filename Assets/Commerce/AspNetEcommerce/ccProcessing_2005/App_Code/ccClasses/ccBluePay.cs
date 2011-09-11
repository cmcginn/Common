using System;

using System.Globalization;
using Westwind.InternetTools;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Westwind.Tools;
using System.Text;

namespace Westwind.WebStore
{

    /// <summary>
    /// This class provides Credit Card processing against the BluePay
    /// Gateway.
    /// </summary>
    public class ccBluePay : ccProcessing
    {

        public ccBluePay()
        {
            this.HttpLink = "https://secure.bluepay.com/bp10emu";
        }

        /// <summary>
        /// Validates the actual card against Authorize.Net Gateway using the HTTP 
        /// interface.
        /// <seealso>Class ccBluePay</seealso>
        /// </summary>
        /// <param name=""></param>
        /// <returns>Boolean</returns>
        public override bool ValidateCard()
        {
            if (!base.ValidateCard())
                return false;

            if (this.Http == null)
            {
                this.Http = new wwHttp();
                this.Http.Timeout = this.Timeout;
            }

            this.Http.AddPostKey("MERCHANT", this.MerchantId);

            string TransactionMode = "LIVE";
            if (this.UseTestTransaction)
                TransactionMode = "TEST";
            this.Http.AddPostKey("MODE",TransactionMode);

            string cOrderAmount = "0.00";

            string AuthType = "SALE";

            // *** Order Amount
            if (this.OrderAmount >= 0)
            {
                if (this.ProcessType == ccProcessTypes.Sale)
                    AuthType = "SALE";
                else if (this.ProcessType == ccProcessTypes.PreAuth)
                    AuthType = "AUTH";
                else if (this.ProcessType == ccProcessTypes.Credit)
                    AuthType = "REFUND";

                cOrderAmount = this.OrderAmount.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                AuthType = "REFUND";
                cOrderAmount = (this.OrderAmount * -1).ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            this.Http.AddPostKey("TRANSACTION_TYPE", AuthType);
            this.Http.AddPostKey("AMOUNT", cOrderAmount);

            // *** Optional Tax Amount 
            if (this.TaxAmount > 0.00M)
                this.Http.AddPostKey("AMOUNT_TAX", this.TaxAmount.ToString(CultureInfo.InvariantCulture.NumberFormat));

            // *** Credit Card Info
            string CardNo = Regex.Replace(this.CreditCardNumber, @"[ -/._#]", "");
            this.Http.AddPostKey("CC_NUM", CardNo);
            this.Http.AddPostKey("CC_EXPIRES", this.CreditCardExpirationMonth + this.CreditCardExpirationYear);

            if (this.SecurityCode != null && this.SecurityCode != "")
                this.Http.AddPostKey("CVCCVV2", this.SecurityCode);


            this.Http.AddPostKey("NAME", this.Firstname.TrimEnd() + " " + this.Lastname.Trim() );
            this.Http.AddPostKey("ADDR1", this.Address);
            this.Http.AddPostKey("CITY", this.City);
            this.Http.AddPostKey("STATE", this.State);
            this.Http.AddPostKey("ZIPCODE", this.Zip);
            

            if (this.Email != null && this.Email != "")
                this.Http.AddPostKey("EMAIL", this.Email);
            if (this.Phone != null && this.Phone != "")
                this.Http.AddPostKey("PHONE", this.Phone);

            this.Http.AddPostKey("ORDER_ID", this.OrderId);
            this.Http.AddPostKey("COMMENT", this.Comment);


            string SealText = this.MerchantPassword + this.MerchantId + AuthType + cOrderAmount + TransactionMode;
;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash( System.Text.Encoding.ASCII.GetBytes(SealText) );
            string EncodedSealText = ByteArrayToBinHex(hash).ToLower();

            this.Http.AddPostKey("TAMPER_PROOF_SEAL", EncodedSealText);
                        
            this.Http.CreateWebRequestObject(this.HttpLink);
            
            // *** Blue pay returns 302 Moved header - with result in querystring
            this.Http.WebRequest.AllowAutoRedirect = false; 

            this.RawProcessorResult = this.Http.GetUrl(this.HttpLink);

            // *** Http Error of some sort
            if (this.Http.Error)
            {
                this.ValidatedResult = "FAILED";
                this.ValidatedMessage = this.Http.ErrorMessage;
                this.SetError(this.Http.ErrorMessage);
                this.LogTransaction();
                return false;
            }

            string AuthResult = this.Http.WebResponse.Headers["Location"];
            AuthResult = wwUtils.ExtractString(AuthResult,"?", "\r",false,true);

            // *** can't parse result - error
            if (AuthResult == "")
            {
                this.ValidatedResult = "FAILED";
                this.ValidatedMessage = "Invalid Result returned";
                this.SetError("Invalid Result returned.");
                return false;
            }

            // *** Re-Assign just the raw query string instead of the HTTP content that contains it
            this.RawProcessorRequest = AuthResult;

            this.ValidatedResult = wwHttpUtils.GetUrlEncodedKey(AuthResult, "RESULT");
            this.ValidatedMessage = wwHttpUtils.GetUrlEncodedKey(AuthResult,"MESSAGE");
            string AVS = wwHttpUtils.GetUrlEncodedKey(AuthResult, "AVS");
            this.TransactionId = wwHttpUtils.GetUrlEncodedKey(AuthResult, "RRNO");

            
            if (this.ValidatedResult == "APPROVED")
            {
                this.ValidatedMessage="Transaction has been approved";
                this.SetError(null);  // Clear any errors
                this.AuthorizationCode = wwHttpUtils.GetUrlEncodedKey(AuthResult,"AUTH_CODE");
            }
            else if (this.ValidatedResult == "ERROR")
            {
                this.ValidatedResult = "FAILED";
                this.SetError(this.ValidatedMessage);
            }
            else if (this.ValidatedResult == "DECLINED")
            {
                this.SetError(this.ValidatedMessage);
            }
            else
            {
                this.SetError(this.ValidatedMessage);
            }

            this.LogTransaction();
            
            return !this.Error;
        }

        //This is used to convert a byte array to a hex string
        private string ByteArrayToBinHex(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }

}
