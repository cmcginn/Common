using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Types.Interfaces
{
    public interface IResponseStatusType
    {
        string Description { get; set; }
        StatusTypes StatusType { get; set; }
        IEnumerable<string> ResponseMessages { get; set; }
    }
}
