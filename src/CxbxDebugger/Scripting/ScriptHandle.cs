// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using CxbxDebugger.Shared;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CxbxDebugger
{
    class ScriptHandle
    {
        static string OnBreakpointMethod = "OnBreakpoint";
        static string OnFileOpenMethod = "OnFileOpen";

        Dictionary<string, MethodInfo> MethodCache;

        public ScriptHandle(Assembly compiledAssembly)
        {
            if (compiledAssembly != null)
            {
                Type userScript = compiledAssembly.GetType("CxbxDebugger.UserScript");
                if (userScript != null)
                {
                    MethodCache = new Dictionary<string, MethodInfo>();

                    CacheScriptBinding(userScript, BindingFlags.Static | BindingFlags.Public, OnBreakpointMethod);
                    CacheScriptBinding(userScript, BindingFlags.Static | BindingFlags.Public, OnFileOpenMethod);
                }
            }
        }

        private void CacheScriptBinding(Type userScript, BindingFlags bindingFlags, string methodName)
        {
            var methodBinding = userScript.GetMethod(methodName, bindingFlags);
            if (methodBinding != null)
            {
                MethodCache.Add(methodName, methodBinding);
            }
        }

        public static ScriptHandle Invalid()
        {
            return new ScriptHandle(null);
        }

        public void OnBreakpoint(DebuggerEngine Engine, uint Address)
        {
            Invoke(OnBreakpointMethod, new object[] { Engine, Address });
        }

        public void OnFileOpen(DebuggerEngine Engine, string Name)
        {
            Invoke(OnFileOpenMethod, new object[] { Engine, Name });
        }

        public void Invoke(string Name, object[] Params)
        {
            MethodInfo Method;
            if (MethodCache.TryGetValue(Name, out Method))
            {
                try
                {
                    Method.Invoke(null, Params);
                }
                catch
                {
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return MethodCache != null;
            }
        }
    };
}
