using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Common.IO.Interfaces
{
    public class RemoteFileInfo:FileSystemInfo
    {
        public FileTypes FileType { get { return FileTypes.RemoteFile; } }  
    }
}
