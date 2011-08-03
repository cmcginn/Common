using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;

namespace Common.Services.Payment
{

    public class PaymentCardData : IPaymentCardData
    {
        private int _ExpirationMonth = 1;
        private int _ExpirationYear = 2007;
        private string _CardNumber = string.Empty;
        private string _SecurityCode = string.Empty;
        private string _CardHolderName = string.Empty;
        private string _CardHolderFirstName = string.Empty;
        private string _CardHolderLastName = string.Empty;
        private string _MaskedCardNumber = string.Empty;

        public IAddressType BillingAddress { get; set; }
        private PaymentCardType _CardType = PaymentCardType.Unknown;

        public int ExpirationMonth
        {
            get { return _ExpirationMonth; }
            set { _ExpirationMonth = ValidateMonth(value); }
        }
        public int ExpirationYear
        {
            get { return _ExpirationYear; }
            set { _ExpirationYear = ValidateYear(value); }
        }
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        public string MaskedCardNumber
        {
            get { return _MaskedCardNumber; }
            set { _MaskedCardNumber = value; }
        }
        public string SecurityCode
        {
            get { return _SecurityCode; }
            set { _SecurityCode = value; }
        }
        public string CardHolderName
        {
            get 
            {
                if (String.IsNullOrWhiteSpace(_CardHolderName) & !String.IsNullOrWhiteSpace(CardHolderLastName) & !String.IsNullOrWhiteSpace(CardHolderFirstName))
                    _CardHolderName = String.Format("{0} {1}", CardHolderFirstName, CardHolderLastName);
                return _CardHolderName; 
            }
            set { _CardHolderName = value; }
        }
        public string CardHolderFirstName
        {
            get
            {
                return _CardHolderFirstName;
            }
            set
            {
                _CardHolderFirstName = value;
            }
        }
        public string CardHolderLastName
        {
            get
            {
                return _CardHolderLastName;
            }
            set
            {
                _CardHolderLastName = value;
            }
        }
        public PaymentCardType CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }

        private int ValidateMonth(int requestedMonth)
        {
            int result = requestedMonth;


            if (result > 12)
            {
                result = 12;
            }

            if (result < 1)
            {
                result = 1;
            }

            return result;
        }
        private int ValidateYear(int requestedYear)
        {
            int result = requestedYear;

            // Make sure we have a four digit number            
            if (result < 1000)
            {
                result += 2000;
            }


            if (result > 9999)
            {
                result = 9999;
            }

            if (result < 2000)
            {
                result = 2000;
            }

            return result;
        }

    }
}
