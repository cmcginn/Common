using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Fax.Interfaces
{
    public class FaxServiceException:System.Exception
    {
        public FaxServiceException(string message) : base(message) { }
    }
}
