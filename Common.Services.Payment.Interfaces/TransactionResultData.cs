using System;
using System.Collections.Generic;
using System.Text;
namespace Common.Services.Payment.Interfaces
{
 
    public class TransactionResultData : ITransactionResultData
    {

        // local variables
        private string _AVSCode = string.Empty;
        private string _AVSCodeDescription = string.Empty;
        private string _CVVCode = string.Empty;
        private string _CVVCodeDescription = string.Empty;
        private string _ResponseCode = string.Empty;
        private string _ResponseCodeDescription = string.Empty;
        private bool _Succeeded = false;
        private List<ITransactionMessage> _Messages = new List<ITransactionMessage>();
        private string _TransactionReferenceNumber = string.Empty;
        private string _TransactionReferenceNumber2 = string.Empty;
                

        // properties
        public string AVSCode
        {
            get { return _AVSCode; }
            set { _AVSCode = value; }
        }
        public string AVSCodeDescription
        {
            get { return _AVSCodeDescription; }
            set { _AVSCodeDescription = value; }
        }
        public string CVVCode
        {
            get { return _CVVCode; }
            set { _CVVCode = value; }
        }
        public string CVVCodeDescription
        {
            get { return _CVVCodeDescription; }
            set { _CVVCodeDescription = value; }
        }
        public string ResponseCode
        {
            get { return _ResponseCode; }
            set { _ResponseCode = value; }
        }
        public string ResponseCodeDescription
        {
            get { return _ResponseCodeDescription; }
            set { _ResponseCodeDescription = value; }
        }
        public bool Suceeded
        {
            get { return _Succeeded; }
            set { _Succeeded = value; }
        }
        public List<ITransactionMessage> Messages
        {
            get { return _Messages; }
            set { _Messages = value; }
        }
        public string TransactionReferenceNumber
        {
            get { return _TransactionReferenceNumber; }
            set { _TransactionReferenceNumber = value; }
        }
        public string TransactionReferenceNumber2
        {
            get { return _TransactionReferenceNumber2; }
            set { _TransactionReferenceNumber2 = value; }
        }

    }
}
