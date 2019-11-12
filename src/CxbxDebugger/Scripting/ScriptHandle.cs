// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System.Reflection;
using CxbxDebugger.Shared;

namespace CxbxDebugger
{
    class ScriptHandle
    {
        MethodInfo GeneratedMethod;

        public ScriptHandle(MethodInfo method)
        {
            GeneratedMethod = method;
        }

        public static ScriptHandle Invalid()
        {
            return new ScriptHandle(null);
        }

        public void Invoke(uint Address, DebuggerEngine Engine)
        {
            try
            {
                GeneratedMethod?.Invoke(null, new object[] { Address, Engine });
            }
            catch
            {
            }
        }

        public bool IsValid
        {
            get
            {
                return GeneratedMethod != null;
            }
        }
    };
}
