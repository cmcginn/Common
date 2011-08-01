//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml.Linq;
//namespace Common.Services.Payment.Gateways.AuthNet.helpers
//{
//    public class CIMGatewayHelper
//    {
//        /// <summary>
//        /// Create a transaciton
//        /// </summary>
//        public static bool CreateTransaction(long profile_id, long payment_profile_id)
//        {
//            bool out_bool = false;

//            createCustomerProfileTransactionRequest request = new createCustomerProfileTransactionRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            profileTransactionType new_trans = new profileTransactionType();

//            profileTransAuthOnlyType new_item = new profileTransAuthOnlyType();
//            new_item.customerProfileId = profile_id.ToString();
//            new_item.customerPaymentProfileId = payment_profile_id.ToString();
//            new_item.amount = 1.00m;

//            orderExType order = new orderExType();
//            order.invoiceNumber = "inv" + (DateTime.Now.Ticks % int.MaxValue).ToString();
//            order.description = "Example item";
//            new_item.order = order;


//            new_trans.Item = new_item;
//            request.transaction = new_trans;

//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            createCustomerProfileTransactionResponse api_response = null;

//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is createCustomerProfileTransactionResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Created Transaction\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_bool;
//            }
//            if (bResult) api_response = (createCustomerProfileTransactionResponse)response;
//            if (api_response != null)
//            {
//                out_bool = true;
//                Console.WriteLine("Created Transaction " + api_response.directResponse);
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }
//            return out_bool;
//        }

//        /// <summary>
//        /// Update the customer profile 
//        /// </summary>
//        /// <param name="in_profile">The profile ID that we are updating</param>
//        /// <returns>If the operation was successfull or not</returns>
//        public static bool UpdateCustomerProfile(customerProfileMaskedType in_profile)
//        {
//            bool out_bool = false;

//            updateCustomerProfileRequest request = new updateCustomerProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);
//            request.profile = new customerProfileExType();
//            request.profile.customerProfileId = in_profile.customerProfileId;
//            request.profile.email = in_profile.email;
//            request.profile.description = in_profile.email;
//            request.profile.merchantCustomerId = in_profile.merchantCustomerId;

//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            updateCustomerProfileResponse api_response = null;

//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is updateCustomerProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Updated Profile\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_bool;
//            }
//            if (bResult) api_response = (updateCustomerProfileResponse)response;
//            if (api_response != null)
//            {
//                out_bool = true;
//                Console.WriteLine("Updated Profile #" + in_profile.customerProfileId);
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_bool;
//        }

//        /// <summary>
//        /// Delete the customer profile
//        /// </summary>
//        /// <param name="profile_id">The profile id that we are deleting</param>
//        /// <returns>The operaiton succeeded or not</returns>
//        public static bool DeleteCustomerProfile(long profile_id)
//        {
//            bool out_bool = false;

//            deleteCustomerProfileRequest request = new deleteCustomerProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);
//            request.customerProfileId = profile_id.ToString();

//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            deleteCustomerProfileResponse api_response = null;

//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is deleteCustomerProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Deleted Profile\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_bool;
//            }
//            if (bResult) api_response = (deleteCustomerProfileResponse)response;
//            if (api_response != null)
//            {
//                out_bool = true;
//                Console.WriteLine("Deleted Profile #" + profile_id);
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_bool;
//        }

//        /// <summary>
//        /// Get the customer profile
//        /// </summary>
//        /// <param name="profile_id">The ID of the customer that we are getting the profile for</param>
//        /// <returns>The profile that was returned</returns>
//        public static customerProfileMaskedType GetCustomerProfile(long profile_id)
//        {
//            customerProfileMaskedType out_profile = null;

//            getCustomerProfileRequest request = new getCustomerProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);
//            request.customerProfileId = profile_id.ToString();

//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            getCustomerProfileResponse api_response = null;

//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is getCustomerProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Retrieved Profile\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_profile;
//            }
//            if (bResult) api_response = (getCustomerProfileResponse)response;
//            if (api_response != null)
//            {
//                out_profile = api_response.profile;
//                Console.WriteLine("Retrieved Profile #" + profile_id);
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_profile;
//        }

//        /// <summary>
//        /// Update the customer address
//        /// </summary>
//        /// <param name="profile_id">The customer profile id we are doing this for</param>
//        /// <param name="AddressID">The payment profile id that we are updating</param>
//        /// <returns></returns>
//        public static bool UpdateCustomerAddress(long profile_id, string AddressID)
//        {
//            bool out_bool = false;

//            // Setup the request
//            updateCustomerShippingAddressRequest request = new updateCustomerShippingAddressRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            // Create a new address record
//            customerAddressExType new_address = new customerAddressExType();
//            new_address.address = "2222 Stree";
//            new_address.city = "Bellevue";
//            new_address.state = "WA";
//            new_address.zip = "98004";

//            // Assign the values to this request
//            request.customerProfileId = profile_id.ToString();
//            request.address = new_address;
//            request.address.customerAddressId = AddressID;

//            // Send the request and get the response
//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            updateCustomerShippingAddressResponse api_response = null;

//            // See what the response was
//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is updateCustomerShippingAddressResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Updated Address\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_bool;
//            }
//            if (bResult) api_response = (updateCustomerShippingAddressResponse)response;
//            if (api_response != null)
//            {
//                out_bool = true;
//                Console.WriteLine("Updated Address: ");
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_bool;
//        }

//        /// <summary>
//        /// Update the customer payment profile
//        /// </summary>
//        /// <param name="profile_id">The customer profile id we are doing this for</param>
//        /// <param name="PaymentProfileID">The payment profile id that we are updating</param>
//        /// <param name="CardNumber">The number for the card that we are processing</param>
//        /// <param name="ExpirationDate">The expiration date for the card that we are processing</param>
//        /// <param name="BillToFirstName">The first name of the person who will be the bill to</param>
//        /// <returns></returns>
//        public static bool UpdateCustomerPaymentProfile(long profile_id, string PaymentProfileID, string CardNumber, string ExpirationDate, string BillToFirstName)
//        {
//            bool out_bool = false;

//            // Setup the request
//            updateCustomerPaymentProfileRequest request = new updateCustomerPaymentProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            // Create a new credit card
//            customerPaymentProfileExType new_payment_profile = new customerPaymentProfileExType();
//            paymentType new_payment = new paymentType();
//            creditCardType new_card = new creditCardType();
//            new_card.cardNumber = CardNumber;
//            new_card.expirationDate = ExpirationDate;
//            new_payment.Item = new_card;
//            new_payment_profile.payment = new_payment;

//            // Create the bill to type
//            new_payment_profile.billTo = new customerAddressType();
//            new_payment_profile.billTo.firstName = BillToFirstName;
//            new_payment_profile.billTo.lastName = "Nelson";
//            new_payment_profile.billTo.company = "Authorize.net";
//            new_payment_profile.billTo.address = "123 bellevue way";
//            new_payment_profile.billTo.city = "bellevue";
//            new_payment_profile.billTo.state = "wa";
//            new_payment_profile.billTo.zip = "98004";
//            new_payment_profile.billTo.country = "us";
//            new_payment_profile.billTo.phoneNumber = "4255551212";
//            new_payment_profile.billTo.faxNumber = "4255551313";

//            // Apply the new card over the existing card
//            request.customerProfileId = profile_id.ToString();
//            request.paymentProfile = new_payment_profile;
//            request.paymentProfile.customerPaymentProfileId = PaymentProfileID;

//            // Send the request and get the response
//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            updateCustomerPaymentProfileResponse api_response = null;

//            // See what the response was
//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is updateCustomerPaymentProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Updated Payment Profile\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_bool;
//            }
//            if (bResult) api_response = (updateCustomerPaymentProfileResponse)response;
//            if (api_response != null)
//            {
//                out_bool = true;
//                Console.WriteLine(String.Format("Updated Payment Profile\n	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_bool;
//        }

//        /// <summary>
//        /// Update the customer payment profile
//        /// </summary>
//        /// <param name="profile_id">The customer profile id we are doing this for</param>
//        /// <param name="PaymentProfileID">The payment profile id that we are updating</param>
//        /// <param name="BankNumber">The BankNumbe for the bank that we are processing</param>
//        /// <param name="RoutingNumber">The RoutingNumber for the bank that we are processing</param>
//        /// <param name="BillToFirstName">The first name of the person who will be the bill to</param>
//        /// <returns></returns>
//        public static bool UpdateCustomerPaymentProfileBank(long profile_id, string PaymentProfileID, string BankNumber, string RoutingNumber, string BillToFirstName)
//        {
//            bool out_bool = false;

//            // Setup the request
//            updateCustomerPaymentProfileRequest request = new updateCustomerPaymentProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            // Create the bank account type
//            customerPaymentProfileExType new_payment_profile = new customerPaymentProfileExType();
//            paymentType new_payment = new paymentType();
//            bankAccountType new_bank = new bankAccountType();
//            new_bank.accountNumber = BankNumber;
//            new_bank.routingNumber = RoutingNumber;
//            new_bank.nameOnAccount = "xyz";
//            new_payment.Item = new_bank;

//            // Create the bill to type
//            new_payment_profile.payment = new_payment;
//            new_payment_profile.billTo = new customerAddressType();
//            new_payment_profile.billTo.firstName = BillToFirstName;
//            new_payment_profile.billTo.lastName = "Nelson";
//            new_payment_profile.billTo.company = "Authorize.net";
//            new_payment_profile.billTo.address = "123 bellevue way";
//            new_payment_profile.billTo.city = "bellevue";
//            new_payment_profile.billTo.state = "wa";
//            new_payment_profile.billTo.zip = "98004";
//            new_payment_profile.billTo.country = "us";
//            new_payment_profile.billTo.phoneNumber = "4255551212";
//            new_payment_profile.billTo.faxNumber = "4255551313";

//            // Apply the new card over the existing card
//            request.customerProfileId = profile_id.ToString();
//            request.paymentProfile = new_payment_profile;
//            request.paymentProfile.customerPaymentProfileId = PaymentProfileID;

//            // Send the request and get the response
//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            updateCustomerPaymentProfileResponse api_response = null;

//            // See what the response was
//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is updateCustomerPaymentProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Updated Payment Profile\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_bool;
//            }
//            if (bResult) api_response = (updateCustomerPaymentProfileResponse)response;
//            if (api_response != null)
//            {
//                out_bool = true;
//                Console.WriteLine(String.Format("Updated Payment Profile\n	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_bool;
//        }

//        /// <summary>
//        /// Create a customer address record for the specified profile
//        /// </summary>
//        /// <param name="profile_id">The customer profile that we are creating a new address for</param>
//        /// <returns>The ID of the profile that was created</returns>
//        public static long CreateCustomerAddress(long profile_id)
//        {
//            long out_id = 0;

//            // Create a new address record
//            customerAddressType new_address = new customerAddressType();
//            new_address.address = "3666 Stree";
//            new_address.city = "Bellevue";
//            new_address.state = "WA";
//            new_address.zip = "98004";

//            // Create a new request
//            createCustomerShippingAddressRequest request = new createCustomerShippingAddressRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            // Assign the values to this request
//            request.customerProfileId = profile_id.ToString();
//            request.address = new_address;

//            // Send the request 
//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            createCustomerShippingAddressResponse api_response = null;

//            // Retrieve the response
//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is createCustomerShippingAddressResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Created Customer Address\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_id;
//            }
//            if (bResult) api_response = (createCustomerShippingAddressResponse)response;
//            if (api_response != null)
//            {
//                out_id = Convert.ToInt64(api_response.customerAddressId);
//                Console.WriteLine("Created Customer Address #" + out_id);
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_id;
//        }

//        /// <summary>
//        /// Create the customer payment profile
//        /// </summary>
//        /// <param name="profile_id">The ID of the customer that we are creating</param>
//        /// <param name="IsBank">If this is a bank or not</param>
//        /// <returns>The return result</returns>
//        public static long CreateCustomerPaymentProfile(long profile_id, bool IsBank)
//        {
//            long out_id = 0;

//            customerPaymentProfileType new_payment_profile = new customerPaymentProfileType();
//            paymentType new_payment = new paymentType();

//            if (IsBank)
//            {
//                bankAccountType new_bank = new bankAccountType();
//                new_bank.nameOnAccount = "xyz";
//                new_bank.accountNumber = "4111111";
//                new_bank.routingNumber = "325070760";
//                new_payment.Item = new_bank;
//            }
//            else
//            {
//                creditCardType new_card = new creditCardType();
//                new_card.cardNumber = "4111111111111111";
//                new_card.expirationDate = "2010-10";
//                new_payment.Item = new_card;
//            }

//            new_payment_profile.payment = new_payment;

//            createCustomerPaymentProfileRequest request = new createCustomerPaymentProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            request.customerProfileId = profile_id.ToString();
//            request.paymentProfile = new_payment_profile;
//            request.validationMode = validationModeEnum.testMode;

//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object response = null;
//            createCustomerPaymentProfileResponse api_response = null;

//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out response);
//            if (!(response is createCustomerPaymentProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)response;
//                Console.WriteLine(String.Format("Created Payment Profile\n	 code: {0}\n	  msg: {1}", ErrorResponse.messages.message[0].code, ErrorResponse.messages.message[0].text));
//                return out_id;
//            }
//            if (bResult) api_response = (createCustomerPaymentProfileResponse)response;
//            if (api_response != null)
//            {
//                out_id = Convert.ToInt64(api_response.customerPaymentProfileId);
//                Console.WriteLine("Created Payment Profile #" + out_id);
//                Console.WriteLine(String.Format("	 code: {0}\n	  msg: {1}", api_response.messages.message[0].code, api_response.messages.message[0].text));
//            }

//            return out_id;
//        }

//        /// <summary>
//        /// Create the customer profile
//        /// </summary>
//        /// <returns>The result of the operation</returns>
//        public static long CreateCustomerProfile(string customerEmail, string customerDescription, out string response)
//        {
//            long out_id = 0;
//            response = string.Empty;
//            createCustomerProfileRequest request = new createCustomerProfileRequest();
//            XmlAPIUtilities.PopulateMerchantAuthentication((ANetApiRequest)request);

//            customerProfileType m_new_cust = new customerProfileType();
//            m_new_cust.email = customerEmail;
//            m_new_cust.description = customerDescription;

//            request.profile = m_new_cust;

//            System.Xml.XmlDocument response_xml = null;
//            bool bResult = XmlAPIUtilities.PostRequest(request, out response_xml);
//            object xmlResponseString = null;

//            createCustomerProfileResponse api_response = null;

//            if (bResult) bResult = XmlAPIUtilities.ProcessXmlResponse(response_xml, out xmlResponseString);
//            if (!(xmlResponseString is createCustomerProfileResponse))
//            {
//                ANetApiResponse ErrorResponse = (ANetApiResponse)xmlResponseString;
//                response = response_xml.OuterXml;
//                return out_id;
//            }
//            if (bResult) api_response = (createCustomerProfileResponse)xmlResponseString;
//            if (api_response != null)
//            {
//                response = response_xml.OuterXml;
//                out_id = Convert.ToInt64(api_response.customerProfileId);
//            }

//            return out_id;
//        }
//    }
//}

