// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using System.Text;

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
            return DebuggerImpl?.ReadMemoryChunk(Address, Size);
        }

        public string ReadString(uint Address, uint blockSize = 256)
        {
            var data = DebuggerImpl?.ReadMemoryChunk(Address, blockSize);
            if (data == null)
                return "";

            var result = Encoding.UTF8.GetString(data);
            var ntIndex = result.IndexOf('\0');
            if (ntIndex != -1)
                return result.Substring(0, ntIndex);

            // there is more data otherwise
            result += " (truncated)";

            return result;
        }

        public StackContext StackContext
        {
            get
            {
                return DebuggerImpl?.GetStackContext();
            }
        }

        public virtual void DebugLog(string Message)
        {
            // stub
        }
    }
}
