using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Reflection;

namespace RepositoryWatcher.Helpers
{
    internal static class RoslynScripting
    {
        internal static Func<T, bool> Evaluate<T>(Assembly[] assemblies, string[] imports, string lambda)
        {
            var options = ScriptOptions.Default.AddReferences(assemblies).WithImports(imports);
            return CSharpScript.EvaluateAsync<Func<T, bool>>(lambda, options).Result;
        }
    }
}
