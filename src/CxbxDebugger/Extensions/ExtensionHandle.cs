// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System.Reflection;

namespace CxbxDebugger.Extensions
{
    class ExtensionHandle
    {
        MethodInfo GeneratedMethod;

        public ExtensionHandle(MethodInfo method)
        {
            GeneratedMethod = method;
        }

        public static ExtensionHandle Invalid()
        {
            return new ExtensionHandle(null);
        }

        public void Invoke()
        {
            GeneratedMethod?.Invoke(null, null);
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
