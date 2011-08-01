using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public class ResponseMessage:IResponseMessage
    {
        public string Response { get; set; }        
    }
}
