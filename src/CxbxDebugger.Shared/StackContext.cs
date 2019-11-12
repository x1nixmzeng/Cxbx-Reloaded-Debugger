// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;

namespace CxbxDebugger.Shared
{
    public class StackContext
    {
        public uint edi { get; protected set; } = 0;
        public uint esi { get; protected set; } = 0;
        public uint ebx { get; protected set; } = 0;
        public uint edx { get; protected set; } = 0;
        public uint ecx { get; protected set; } = 0;
        public uint eax { get; protected set; } = 0;
        public uint ebp { get; protected set; } = 0;
        public uint eip { get; protected set; } = 0;
        public uint esp { get; protected set; } = 0;

        public IntPtr PC { get { return new IntPtr(eip); } }
        public IntPtr Base { get { return new IntPtr(ebp); } }
        public IntPtr Stack { get { return new IntPtr(esp); } }

        public StackContext()
        { }

        public StackContext(uint eip, uint ebp)
        {
            this.eip = eip;
            this.ebp = ebp;
        }
    }
}
