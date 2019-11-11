// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace CxbxDebugger.Extensions
{
    class ExtensionManager
    {
        CSharpCodeProvider CodeProvider;
        CompilerParameters CompilerParams;

        public ExtensionManager()
        {
            CodeProvider = new CSharpCodeProvider();
            CompilerParams = new CompilerParameters();

            CompilerParams.GenerateInMemory = true;
            CompilerParams.TreatWarningsAsErrors = true; // strict mode

            CompilerParams.ReferencedAssemblies.Add("System.dll");
        }

        public ExtensionHandle Compile(string[] Sources)
        {
            ExtensionHandle handle = ExtensionHandle.Invalid();

            CompilerResults results = CodeProvider.CompileAssemblyFromSource(CompilerParams, Sources);

            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
            }
            else
            {
                var cls = results.CompiledAssembly.GetType("CxbxDebugger.ExtensionClass");

                var method = cls.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);
                handle = new ExtensionHandle(method);
            }

            return handle;
        }
    }
}
