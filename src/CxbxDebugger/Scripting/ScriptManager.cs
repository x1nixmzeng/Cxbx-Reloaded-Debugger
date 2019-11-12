// Written by x1nixmzeng for the Cxbx-Reloaded project
//

using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Collections.Generic;

namespace CxbxDebugger
{
    struct ScriptCompilerError
    {
        public string Error;

        public string FileName;
        public int Line;
    }

    class ScriptManager
    {
        CSharpCodeProvider CodeProvider;
        CompilerParameters CompilerParams;

        public ScriptManager()
        {
            // todo: investigate missing csc dep:
            //var providerOptions = new Dictionary<string, string>();
            //providerOptions.Add("CompilerVersion", "v4.6");

            //CodeProvider = new CSharpCodeProvider(providerOptions);

            CodeProvider = new CSharpCodeProvider();
            CompilerParams = new CompilerParameters();

            CompilerParams.GenerateInMemory = true;
            CompilerParams.TreatWarningsAsErrors = true; // strict mode

            CompilerParams.ReferencedAssemblies.Add("System.dll");
            CompilerParams.ReferencedAssemblies.Add("CxbxDebugger.Shared.dll");
        }

        public ScriptHandle Compile(string[] Sources, ref ScriptCompilerError[] Errors)
        {
            ScriptHandle handle = ScriptHandle.Invalid();

            CompilerResults results = CodeProvider.CompileAssemblyFromSource(CompilerParams, Sources);

            if (results.Errors.HasErrors)
            {
                var errorList = new List<ScriptCompilerError>();

                foreach (CompilerError error in results.Errors)
                {
                    ScriptCompilerError compileError;
                    compileError.Error = error.ErrorText;
                    compileError.Line = error.Line;
                    compileError.FileName = error.FileName;

                    errorList.Add(compileError);
                }

                Errors = errorList.ToArray();
            }
            else
            {
                var cls = results.CompiledAssembly.GetType("CxbxDebugger.UserScript");

                var method = cls.GetMethod("OnBreakpoint", BindingFlags.Static | BindingFlags.Public);
                handle = new ScriptHandle(method);
            }

            return handle;
        }
    }
}
