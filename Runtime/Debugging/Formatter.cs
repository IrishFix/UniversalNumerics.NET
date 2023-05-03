using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Debugging {
    public static class Formatter {
        private const string indentString = "    ";
        private static string FormatToExpandedJson(string json) {
            int indentation = 0;
            int quoteCount = 0;
            IEnumerable<string> result = 
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine +  string.Concat(Enumerable.Repeat(indentString, indentation)) : null
                let openChar = ch is '{' or '[' ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indentString, ++indentation)) : ch.ToString()
                let closeChar = ch is '}' or ']' ? Environment.NewLine + string.Concat(Enumerable.Repeat(indentString, --indentation)) + ch : ch.ToString()
                select lineBreak ?? (openChar.Length > 1 
                    ? openChar 
                    : closeChar);
            return string.Concat(result);
        }
        
        public static string ExpandJson(string FlatJson) {
            return FormatToExpandedJson(FlatJson);
        }
    }
}