using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Fax.Interfaces
{
    public interface IValidatedType
    {
        Dictionary<string, FileSystemInfo> ValidationSchemaInfo { get; set; }
        void Validate(string tag);
    }
}
