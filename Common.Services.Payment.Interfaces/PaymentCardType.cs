using System;
using System.Collections.Generic;
using System.Text;
namespace Common.Services.Payment.Interfaces
{
    public enum PaymentCardType
    {
        Unknown,
        Amex,
        Discover,
        DinersClub,
        JCB,
        Maestro,
        MasterCard,
        Solo,
        Switch,
        Visa,
        Other
    }
}
