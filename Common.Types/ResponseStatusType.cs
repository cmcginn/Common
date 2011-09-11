using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Types.Interfaces;
namespace Common.Types
{
    public class ResponseStatusType:IResponseStatusType
    {
        public string Description { get; set; }
        public StatusTypes StatusType { get; set; }
        public IEnumerable<string> ResponseMessages { get; set; }
    }
}
