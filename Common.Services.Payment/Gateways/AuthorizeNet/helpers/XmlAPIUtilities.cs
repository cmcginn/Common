///*
// * Before working with this sample code, please be sure to read the accompanying Readme.txt file.
// * It contains important information regarding the appropriate use of and conditions for this sample
// * code. Also, please pay particular attention to the comments included in each individual code file,
// * as they will assist you in the unique and correct implementation of this code on your specific platform.
// *
// * Copyright 2008 Authorize.Net Corp.
// */

//using System;
//using System.Text;
//using System.IO;
//using System.Net;
//using System.Xml;
//using System.Xml.Serialization;


//namespace Common.Services.Payment.Gateways.AuthNet.helpers
//{
//    public class XmlAPIParams
//    {
//        public string AccountName { get; set; }
//        public string MerchantName { get; set; }
//        public string TransactionKey { get; set; }
//        public string ApiUrl { get; set; }
//    }
//    public class XmlAPIUtilities
//    {
       
//        public static bool PRINT_POST = true;

//        // TODO: Specify the URL to the API as https://api.authorize.net/xml/v1/request.api
//        //
//        public static String API_URL = Properties.AuthorizeNet.Default.APIUrl;

//        // TODO: Specify you merchant API Login ID
//        //
//        public static String EXAMPLE_MERCHANT_NAME = Properties.AuthorizeNet.Default.APIAccountName;

//        // TODO: Specify you merchant transaction key
//        //
//        public static String EXAMPLE_TRANSACTION_KEY = Properties.AuthorizeNet.Default.APIAccountPassword;

//        private static merchantAuthenticationType m_auth = null;

//        public static merchantAuthenticationType MerchantAuthentication
//        {
//            get
//            {
//                if (m_auth == null)
//                {
//                    m_auth = new merchantAuthenticationType();
//                    m_auth.name = EXAMPLE_MERCHANT_NAME;
//                    m_auth.transactionKey = EXAMPLE_TRANSACTION_KEY;
//                }
//                return m_auth;
//            }
//        }
//        public static void PopulateMerchantAuthentication(ANetApiRequest request)
//        {
//            request.merchantAuthentication = new merchantAuthenticationType();
//            request.merchantAuthentication.name = EXAMPLE_MERCHANT_NAME;
//            request.merchantAuthentication.transactionKey = EXAMPLE_TRANSACTION_KEY;
//        }
//        public static bool ProcessXmlResponse(XmlDocument xmldoc, out object apiResponse)
//        {
//            bool bResult = true;
//            XmlSerializer serializer;

//            apiResponse = null;

//            try
//            {
//                // Use the root node to determine the type of response object to create
//                switch (xmldoc.DocumentElement.Name)
//                {
//                    case "createCustomerPaymentProfileResponse":
//                        serializer = new XmlSerializer(typeof(createCustomerPaymentProfileResponse));
//                        apiResponse = (createCustomerPaymentProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "createCustomerProfileResponse":
//                        serializer = new XmlSerializer(typeof(createCustomerProfileResponse));
//                        apiResponse = (createCustomerProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "createCustomerProfileTransactionResponse":
//                        serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
//                        apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "createCustomerShippingAddressResponse":
//                        serializer = new XmlSerializer(typeof(createCustomerShippingAddressResponse));
//                        apiResponse = (createCustomerShippingAddressResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "deleteCustomerPaymentProfileResponse":
//                        serializer = new XmlSerializer(typeof(deleteCustomerPaymentProfileResponse));
//                        apiResponse = (deleteCustomerPaymentProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "deleteCustomerProfileResponse":
//                        serializer = new XmlSerializer(typeof(deleteCustomerProfileResponse));
//                        apiResponse = (deleteCustomerProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "deleteCustomerShippingAddressResponse":
//                        serializer = new XmlSerializer(typeof(deleteCustomerShippingAddressResponse));
//                        apiResponse = (deleteCustomerShippingAddressResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "getCustomerPaymentProfileResponse":
//                        serializer = new XmlSerializer(typeof(getCustomerPaymentProfileResponse));
//                        apiResponse = (getCustomerPaymentProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "getCustomerProfileIdsResponse":
//                        serializer = new XmlSerializer(typeof(getCustomerProfileIdsResponse));
//                        apiResponse = (getCustomerProfileIdsResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "getCustomerProfileResponse":
//                        serializer = new XmlSerializer(typeof(getCustomerProfileResponse));
//                        apiResponse = (getCustomerProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "getCustomerShippingAddressResponse":
//                        serializer = new XmlSerializer(typeof(getCustomerShippingAddressResponse));
//                        apiResponse = (getCustomerShippingAddressResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "isAliveResponse":
//                        serializer = new XmlSerializer(typeof(isAliveResponse));
//                        apiResponse = (isAliveResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "updateCustomerPaymentProfileResponse":
//                        serializer = new XmlSerializer(typeof(updateCustomerPaymentProfileResponse));
//                        apiResponse = (updateCustomerPaymentProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "updateCustomerProfileResponse":
//                        serializer = new XmlSerializer(typeof(updateCustomerProfileResponse));
//                        apiResponse = (updateCustomerProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "updateCustomerShippingAddressResponse":
//                        serializer = new XmlSerializer(typeof(updateCustomerShippingAddressResponse));
//                        apiResponse = (updateCustomerShippingAddressResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;
//                    case "validateCustomerPaymentProfileResponse":
//                        serializer = new XmlSerializer(typeof(validateCustomerPaymentProfileResponse));
//                        apiResponse = (validateCustomerPaymentProfileResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;

//                    case "ErrorResponse":
//                        serializer = new XmlSerializer(typeof(ANetApiResponse));
//                        apiResponse = (ANetApiResponse)serializer.Deserialize(new StringReader(xmldoc.DocumentElement.OuterXml));
//                        break;

//                    default:
//                        Console.WriteLine("Unexpected type of object: " + xmldoc.DocumentElement.Name);
//                        bResult = false;
//                        break;
//                }
//            }
//            catch (Exception ex)
//            {
//                bResult = false;
//                apiResponse = null;
//                Console.WriteLine(ex.GetType().ToString() + ": " + ex.Message);
//            }

//            return bResult;
//        }
//        public static void ProcessResponse(object response)
//        {
//            // Every response is based on ANetApiResponse so you can always do this sort of type casting.
//            ANetApiResponse baseResponse = (ANetApiResponse)response;

//            // Write the results to the console window
//            Console.Write("Result: ");
//            Console.WriteLine(baseResponse.messages.resultCode.ToString());

//            // If the result code is "Ok" then the request was successfully processed.
//            if (baseResponse.messages.resultCode == messageTypeEnum.Ok)
//            {
//                // CreateSubscription is the only method that returns additional data
//                if (response.GetType() == typeof(createCustomerProfileResponse))
//                {
//                    createCustomerProfileResponse createResponse = (createCustomerProfileResponse)response;
//                    // _subscriptionId = createResponse.subscriptionId;

//                    // Console.WriteLine("Subscription ID: " + _subscriptionId);
//                }
//            }
//            else
//            {
//                // Write error messages to console window
//                for (int i = 0; i < baseResponse.messages.message.Length; i++)
//                {
//                    Console.WriteLine("[" + baseResponse.messages.message[i].code
//                            + "] " + baseResponse.messages.message[i].text);
//                }
//            }
//        }
//        public static bool PostRequest(object ApiRequest, out XmlDocument xmldoc)
//        {
//            bool bResult = false;
//            XmlSerializer serializer;

//            xmldoc = null;

//            try
//            {
//                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(API_URL);
//                webRequest.Method = "POST";
//                webRequest.ContentType = "text/xml";
//                webRequest.KeepAlive = true;

//                // Serialize the request
//                serializer = new XmlSerializer(ApiRequest.GetType());
//                XmlWriter writer = new XmlTextWriter(webRequest.GetRequestStream(), Encoding.UTF8);
//                serializer.Serialize(writer, ApiRequest);
//                writer.Close();

//                if (PRINT_POST)
//                {
//                    Console.WriteLine("\r\n\r\n");
//                    StreamWriter consoleOutput = new StreamWriter(Console.OpenStandardOutput());
//                    consoleOutput.AutoFlush = true;
//                    serializer.Serialize(consoleOutput, ApiRequest);
//                    consoleOutput.Close();
//                    Console.WriteLine("\r\n\r\n");
//                }

//                // Get the response
//                WebResponse webResponse = webRequest.GetResponse();

//                // Load the response from the API server into an XmlDocument.
//                xmldoc = new XmlDocument();
//                xmldoc.Load(XmlReader.Create(webResponse.GetResponseStream()));

//                if (PRINT_POST)
//                {
//                    XmlWriterSettings settings = new XmlWriterSettings();
//                    settings.Indent = true;
//                    settings.Encoding = Encoding.ASCII;
//                    XmlWriter consoleWriter = XmlWriter.Create(Console.Out, settings);
//                    xmldoc.WriteTo(consoleWriter);
//                    consoleWriter.Close();
//                    Console.WriteLine("\r\n\r\n");
//                }

//                bResult = true;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.GetType().ToString() + ": " + ex.Message);
//                bResult = false;
//            }

//            return bResult;
//        }
//    }
//}
