using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public enum TransactionMessageType
    {
        Unknown, 
        Error, 
        Warning, 
        Information
    }
}
