using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.ExternalsExample.FileMonitoring
{
    public enum FileEvtType
    {
        FileCreated,
        FileModified,
        FileRenamed,
        DirectoryCreated,
        DirectoryRenamed,
        ItemDeleted
    }
}
