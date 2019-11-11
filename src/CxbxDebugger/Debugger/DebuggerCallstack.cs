// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using System.Collections.Generic;

namespace CxbxDebugger
{
    public class DebuggerStackFrame
    {
        public uint edi { get; } = 0;
        public uint esi { get; } = 0;
        public uint ebx { get; } = 0;
        public uint edx { get; } = 0;
        public uint ecx { get; } = 0;
        public uint eax { get; } = 0;
        public uint ebp { get; } = 0;
        public uint eip { get; } = 0;
        public uint esp { get; } = 0;

        public IntPtr PC { get { return new IntPtr(eip); } }
        public IntPtr Base { get { return new IntPtr(ebp); } }
        public IntPtr Stack { get { return new IntPtr(esp); } }

        public DebuggerStackFrame()
        { }

        public DebuggerStackFrame(CONTEXT_x86 context)
        {
            edi = context.edi;
            esi = context.esi;
            ebx = context.ebx;
            edx = context.edx;
            ecx = context.ecx;
            eax = context.eax;
            ebp = context.ebp;
            eip = context.eip;
            esp = context.esp;
        }

        public DebuggerStackFrame(uint eip, uint ebp)
        {
            this.eip = eip;
            this.ebp = ebp;
        }
    }

    public class DebuggerCallstack
    {
        int NumSupportedFrames = 16;
        public List<DebuggerStackFrame> StackFrames { get; }

        public DebuggerCallstack()
        {
            StackFrames = new List<DebuggerStackFrame>(NumSupportedFrames);
        }

        public DebuggerCallstack(int MaxFrames)
        {
            NumSupportedFrames = MaxFrames;
            StackFrames = new List<DebuggerStackFrame>(NumSupportedFrames);
        }

        public void AddFrame(DebuggerStackFrame StackFrame)
        {
            if (CanCollect)
            {
                StackFrames.Add(StackFrame);
            }
        }
        
        public bool CanCollect
        {
            get
            {
                return StackFrames.Count < NumSupportedFrames;
            }
        }
    }
}
