// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;

namespace CxbxDebugger.Shared
{
    public class DebuggerEngine
    {
        IDebugger DebuggerImpl;

        public DebuggerEngine(IDebugger debugger)
        {
            DebuggerImpl = debugger;
        }

        public byte[] ReadMemory(uint Address, uint Size)
        {
            if (DebuggerImpl != null)
            {
                return DebuggerImpl.ReadMemoryChunk(Address, Size);
            }

            throw new Exception("No debugger implementation");
        }
    }
}
