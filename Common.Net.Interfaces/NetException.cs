using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Net.Interfaces
{
    public class NetException:System.Exception
    {
        public NetException(string message) : base(message) { }
        public NetException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
