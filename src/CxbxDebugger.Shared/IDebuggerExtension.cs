// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CxbxDebugger.Shared
{
    public interface IDebugger
    {
        byte[] ReadMemoryChunk(uint Address, uint Size);
        bool WriteMemoryChunk(uint Address, byte[] data);        
    }
}
