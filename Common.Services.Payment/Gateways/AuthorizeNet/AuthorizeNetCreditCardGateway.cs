using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Web;
namespace Common.Services.Payment.Gateways
{
    public class AuthorizeNetCreditCardGateway : BaseCreditCardGateway
    {
        public const string GATEWAY_ID_STRING = "CCDA02FC-C3BD-441b-A5A9-391B00C73670";
        public const string GATEWAY_NAME = "Authorize.Net";
        public override Guid GatewayId { get { return new Guid(GATEWAY_ID_STRING); } }
        public override string GatewayName { get { return GATEWAY_NAME; } }
        public override bool SupportsAuthorize { get { return true; } }
        public override bool SupportsCapture { get { return true; } }
        public override bool SupportsCharge { get { return true; } }
        public override bool SupportsRefund { get { return true; } }
        public override bool SupportsVoid { get { return true; } }
        public override bool SupportsProfile { get { return false; } }

        public override bool Authorize(IPaymentData data)
        {
            return ProcessCard("a", data);
        }
        public override bool Capture(IPaymentData data)
        {
            return ProcessCard("p", data);
        }

        public override bool Charge(IPaymentData data)
        {
            return ProcessCard("c", data);
        }

        public override bool Refund(IPaymentData data)
        {
            //Authorize.NET refund is a credit
            return ProcessCard("c", data);
        }

        public override bool Void(IPaymentData data)
        {
            return ProcessCard("v", data);
        }


        private bool ProcessCard(string chargeType, IPaymentData data)
        {

            bool result = false;

            try
            {
                // Get Processor Settings   
                data.GatewaySettings.UrlLive = new Uri("https://secure.authorize.net/gateway/transact.dll");
                data.GatewaySettings.UrlTest = new Uri("https://test.authorize.net/gateway/transact.dll");
                // Set Server
                string serverUrl = data.GatewaySettings.UrlTest.ToString();
                if (!data.GatewaySettings.TestMode)
                {
                    serverUrl = data.GatewaySettings.UrlLive.ToString();
                }

                // Build Data String
                // Card Number and Expiration
                string expDate = string.Empty;
                if (data.CardData.ExpirationMonth < 10)
                {
                    expDate = "0" + data.CardData.ExpirationMonth.ToString();
                }
                else
                {
                    expDate = data.CardData.ExpirationMonth.ToString();
                }
                if (data.CardData.ExpirationYear > 99)
                {
                    expDate += data.CardData.ExpirationYear.ToString().Substring(2, 2);
                }
                else
                {
                    expDate += data.CardData.ExpirationYear.ToString();
                }


                // Set Parameters
                StringBuilder sb = new StringBuilder();
                string postData = string.Empty;

                sb.Append("x_version=3.1");
                sb.Append("&x_login=");
                sb.Append(SafeWriteString(data.GatewaySettings.Username.Trim()));
                sb.Append("&x_tran_key=");
                sb.Append(SafeWriteString(data.GatewaySettings.Password.Trim()));
                sb.Append("&x_Amount=");
                sb.Append(SafeWriteString(data.Transaction.Amount.ToString()));
                sb.Append("&x_Cust_ID=");
                sb.Append(SafeWriteString(data.Customer.CustomerId));
                sb.Append("&x_Description=");
                sb.Append(SafeWriteString(data.Transaction.MerchantDescription));
                sb.Append("&x_invoice_num=");
                sb.Append(SafeWriteString(data.Transaction.MerchantInvoiceNumber));
                sb.Append("&x_Email_Customer=");
                sb.Append(SafeWriteBool(data.GatewaySettings.EmailCustomer));
                sb.Append("&x_delim_data=");
                sb.Append(SafeWriteBool(true));
                sb.Append("&x_ADC_URL=");
                sb.Append(SafeWriteBool(false));
                sb.Append("&x_delim_char=");
                sb.Append(SafeWriteString(","));
                sb.Append("&x_relay_response=");
                sb.Append(SafeWriteBool(false));
                sb.Append("&x_Email=");
                sb.Append(SafeWriteString(data.Customer.EmailAddress));
                sb.Append("&x_Customer_IP=");
                sb.Append(SafeWriteString(data.Customer.IPAddress));
                sb.Append("&x_First_Name=");
                sb.Append(SafeWriteString(data.Customer.FirstName));
                sb.Append("&x_Last_Name=");
                sb.Append(SafeWriteString(data.Customer.LastName));
                sb.Append("&x_Company=");
                sb.Append(SafeWriteString(data.Customer.Company));
                sb.Append("&x_Address=");
                sb.Append(SafeWriteString(data.Customer.AddressLine1));
                sb.Append("&x_City=");
                sb.Append(SafeWriteString(data.Customer.City));


                // TODO: Convert country codes to ISO Codes or
                // find a way to guarantee that we're getting an iso code
                sb.Append("&x_Country=");
                sb.Append(SafeWriteString(data.Customer.Country));

                // TODO: Add code to make sure we've got the correct state format
                if (data.Customer.State != string.Empty)
                {
                    sb.Append("&x_State=");
                    sb.Append(SafeWriteString(data.Customer.State));
                }

                sb.Append("&x_Zip=");
                sb.Append(SafeWriteString(data.Customer.PostalCode));
                sb.Append("&x_Phone=");
                sb.Append(SafeWriteString(data.Customer.PhoneNumber));

                sb.Append("&x_Method=");
                sb.Append(SafeWriteString("CC"));

                // Add Test Mode Flag if needed
                if (data.GatewaySettings.TestMode)
                {
                    sb.Append(SafeWriteString("&x_test_request=TRUE"));
                }

                switch (chargeType)
                {
                    case "s":
                        // Charge
                        sb.Append("&x_Type=");
                        sb.Append(SafeWriteString("AUTH_CAPTURE"));
                        sb.Append("&x_Card_Num=");
                        sb.Append(SafeWriteString(data.CardData.CardNumber));
                        if (data.CardData.SecurityCode.Length > 0)
                        {
                            sb.Append("&x_Card_Code=");
                            sb.Append(SafeWriteString(data.CardData.SecurityCode));
                        }
                        sb.Append("&x_Exp_Date=");
                        sb.Append(SafeWriteString(expDate));
                        break;
                    case "a":
                        // Authorize
                        sb.Append("&x_Type=");
                        sb.Append(SafeWriteString("AUTH_ONLY"));
                        sb.Append("&x_Card_Num=");
                        sb.Append(SafeWriteString(data.CardData.CardNumber));
                        if (data.CardData.SecurityCode.Length > 0)
                        {
                            sb.Append("&x_Card_Code=");
                            sb.Append(SafeWriteString(data.CardData.SecurityCode));
                        }
                        sb.Append("&x_Exp_Date=");
                        sb.Append(SafeWriteString(expDate));
                        break;
                    case "p":
                        // Capture, Post Authorize
                        sb.Append("&x_Type=");
                        sb.Append(SafeWriteString("PRIOR_AUTH_CAPTURE"));
                        sb.Append("&x_trans_id=");
                        sb.Append(SafeWriteString(data.Transaction.PreviousTransactionReferenceNumber));
                        sb.Append("&x_Card_Num=");
                        sb.Append(SafeWriteString(data.CardData.CardNumber));
                        if (data.CardData.SecurityCode.Length > 0)
                        {
                            sb.Append("&x_Card_Code=");
                            sb.Append(SafeWriteString(data.CardData.SecurityCode));
                        }
                        sb.Append("&x_Exp_Date=");
                        sb.Append(SafeWriteString(expDate));
                        break;
                    case "v":
                        // Void
                        sb.Append("&x_Type=");
                        sb.Append(SafeWriteString("VOID"));
                        sb.Append("&x_trans_id=");
                        sb.Append(SafeWriteString(data.Transaction.PreviousTransactionReferenceNumber));
                        sb.Append("&x_Card_Num=");
                        sb.Append(SafeWriteString(data.CardData.CardNumber));
                        if (data.CardData.SecurityCode.Length > 0)
                        {
                            sb.Append("&x_Card_Code=");
                            sb.Append(SafeWriteString(data.CardData.SecurityCode));
                        }
                        sb.Append("&x_Exp_Date=");
                        sb.Append(SafeWriteString(expDate));
                        break;
                    case "c":
                        // Refund, Credit
                        sb.Append("&x_Type=");
                        sb.Append(SafeWriteString("CREDIT"));
                        sb.Append("&x_trans_id=");
                        sb.Append(SafeWriteString(data.Transaction.PreviousTransactionReferenceNumber));
                        sb.Append("&x_Card_Num=");
                        sb.Append(SafeWriteString(data.CardData.CardNumber));
                        if (data.CardData.SecurityCode.Length > 0)
                        {
                            sb.Append("&x_Card_Code=");
                            sb.Append(SafeWriteString(data.CardData.SecurityCode));
                        }
                        sb.Append("&x_Exp_Date=");
                        sb.Append(SafeWriteString(expDate));
                        break;
                }

                // Dump string builder to string to send to Authorize.Net
                postData = sb.ToString();

                string responseString = Utilities.SendRequestByPost(serverUrl, postData);

                // Split response string
                string[] output = responseString.Split(',');

                int counter = 0;
                System.Collections.Hashtable vars = new System.Collections.Hashtable();

                // Move strings into hash table for easy reference
                foreach (string var in output)
                {
                    vars.Add(counter, var);
                    counter += 1;
                }

                if (vars.Count < 7)
                {
                    result = false;
                }
                else
                {
                    string responseCode = (string)vars[0];
                    string responseDescription = (string)vars[3];
                    string responseAuthCode = (string)vars[4];
                    string responseAVSCode = (string)vars[5];
                    string responseAVSMessage = ParseAvsCode(responseAVSCode);
                    string responseReferenceCode = (string)vars[6];

                    string responseSecurityCode = string.Empty;
                    if (vars.Count > 38)
                    {
                        responseSecurityCode = (string)vars[38];
                    }
                    string responseSecurityCodeMessage = ParseSecurityCode(responseSecurityCode);


                    // Trim off Extra Quotes on response codes
                    responseCode = responseCode.Trim('"');

                    // Save result information to payment data object 
                    data.TransactionResult.ResponseCode = responseCode;
                    data.TransactionResult.ResponseCodeDescription = responseDescription;
                    data.TransactionResult.AVSCode = responseAuthCode;
                    data.TransactionResult.AVSCodeDescription = responseAVSMessage;
                    data.TransactionResult.CVVCode = responseSecurityCode;
                    data.TransactionResult.CVVCodeDescription = responseSecurityCodeMessage;
                    data.TransactionResult.TransactionReferenceNumber = responseReferenceCode;


                    switch (responseCode)
                    {
                        case "1":
                            // Approved
                            result = true;
                            break;
                        case "2":
                            // Declined
                            result = false;
                            data.TransactionResult.Messages.Add(new TransactionMessage("Declined: " + responseDescription, responseCode, TransactionMessageType.Warning));
                            break;
                        case "3":
                            // UNKNOWN
                            result = false;
                            data.TransactionResult.Messages.Add(new TransactionMessage("Authorize.Net Error: " + responseDescription, responseCode, TransactionMessageType.Error));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                data.TransactionResult.Messages.Add(new TransactionMessage("Unknown Payment Error: " + ex.Message, "MBAN1001", TransactionMessageType.Error));
                data.TransactionResult.Messages.Add(new TransactionMessage("Stack Trace " + ex.StackTrace, "STACKTRACE", TransactionMessageType.Information));
            }

            // Also report result as part of Transaction Result Data
            data.TransactionResult.Suceeded = result;

            return result;
        }
        private string SafeWriteBool(bool input)
        {
            if (input)
            {
                return HttpUtility.UrlEncode("TRUE");
            }
            else
            {
                return HttpUtility.UrlEncode("FALSE");
            }
        }
        private string SafeWriteString(string input)
        {
            return HttpUtility.UrlEncode(input);
        }
        private string ParseAvsCode(string code)
        {
            string result = "Uknown";

            switch (code.ToUpper())
            {
                case "A":
                    result = " [AVS - Address (Street) matches, ZIP does not]";
                    break;
                case "B":
                    result = " [AVS - Address information not provided for AVS check]";
                    break;
                case "E":
                    result = " [AVS - Error]";
                    break;
                case "G":
                    result = " [AVS - Non-U.S. Card Issuing Bank]";
                    break;
                case "N":
                    result = " [AVS - No Match on Address (Street) or ZIP]";
                    break;
                case "P":
                    result = " [AVS - AVS not applicable for this transaction]";
                    break;
                case "R":
                    result = " [AVS - Retry – System unavailable or timed out]";
                    break;
                case "S":
                    result = " [AVS - Service not supported by issuer]";
                    break;
                case "U":
                    result = " [AVS - Address information is unavailable]";
                    break;
                case "W":
                    result = " [AVS - 9 digit ZIP matches, Address (Street) does not]";
                    break;
                case "X":
                    result = " [AVS - Address (Street) and 9 digit ZIP match]";
                    break;
                case "Y":
                    result = " [AVS - Address (Street) and 5 digit ZIP match]";
                    break;
                case "Z":
                    result = " [AVS - 5 digit ZIP matches, Address (Street) does not]";
                    break;
            }

            return result;
        }
        private string ParseSecurityCode(string code)
        {
            string result = "Uknown";
            switch (code.ToUpper())
            {
                case "M":
                    result = " [CVV - Match]";
                    break;
                case "N":
                    result = " [CVV - No Match]";
                    break;
                case "P":
                    result = " [CVV - Not Processed]";
                    break;
                case "S":
                    result = " [CVV - Should have been present]";
                    break;
                case "U":
                    result = " [CVV - Issuer unable to process request]";
                    break;
            }
            return result;
        }

    }
}
