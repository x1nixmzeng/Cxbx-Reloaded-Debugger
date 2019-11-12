// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using System.Collections.Generic;
using CxbxDebugger.Shared;

namespace CxbxDebugger
{
    public class DebuggerStackFrame : StackContext
    {
        public DebuggerStackFrame(uint eip, uint ebp)
            : base(eip, ebp)
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
