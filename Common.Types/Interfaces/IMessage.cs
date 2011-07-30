using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Types.Interfaces
{
    public interface IMessage<T>
    {
        string TraceId { get; set; }
        T Payload { get; set; }
    }
}
