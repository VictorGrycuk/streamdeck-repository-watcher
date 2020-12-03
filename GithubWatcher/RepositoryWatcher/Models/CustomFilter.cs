using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryWatcher.Models
{
    public class CustomFilter
    {
        private string[] Imports { get; set; }
        private string[] Expressions { get; set; }

        public static CustomFilter CreateNewCustomFilter(string source)
        {
            return !string.IsNullOrEmpty(source)
                ? new CustomFilter(source)
                : null;
        }

        private CustomFilter(string source)
        {
            var lines = source.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // There has to be at least two lines, one for the imports and another ofr the expressions
            if (lines.Count() < 2)
                throw new ArgumentException("Either no imports or no expressions were found");

            Imports = ParseImports(lines[0]);
            Expressions = lines.Skip(1).ToArray();
        }

        private static string[] ParseImports(string source)
        {
            var imports = source.Split(',');
            
            return imports.Count() > 0
                ? imports
                : throw new ArgumentException("No import directives found for the custom filters");
        }

        private List<T> Filter<T>(List<T> items, string lambda, string[] imports)
        {
            var assembly = new System.Reflection.Assembly[] { typeof(T).Assembly };
            var options = ScriptOptions.Default.AddReferences(assembly).WithImports(imports);
            var filterExpression = CSharpScript.EvaluateAsync<Func<T, bool>>(lambda, options).Result;

            return items.Where(filterExpression).ToList();
        }

        public List<T> ApplyFilters<T>(List<T> items)
        {
            foreach (var expression in Expressions)
            {
                items = Filter(items, expression, Imports);
            }

            return items;
        }
    }
}
