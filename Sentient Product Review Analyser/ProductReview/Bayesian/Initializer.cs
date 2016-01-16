using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProductReview.Bayesian
{
    public abstract class Initializer : IEnumerable<string>
    {
        // Constructor
        public Initializer()
        {

        }

        // Get Tokenize the content
        public static Initializer GetStringsFrom(string content)
        {
            return new StringInitializer(content);
        }

        public abstract IEnumerator<string> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class StringInitializer : Initializer
        {
            private IEnumerable<string> TotalTokens;

            public StringInitializer(string Content)
            {
                TotalTokens = ParseContent(Content);
            }

            public override IEnumerator<string> GetEnumerator()
            {
                return TotalTokens.GetEnumerator();
            }

            // Convert the string to tokens
            private static IEnumerable<string> ParseContent(string str)
            {
                
                string ClearOP = ClearString(str);
                string[] TotalTokens = ClearOP.Split(' ');
                return TotalTokens
                    .Where(X => !X.Equals(" ", StringComparison.InvariantCultureIgnoreCase))
                    .Select(X => X.ToLowerInvariant())
                    .Distinct();
            }

            // The charecters which are not valid, turn them to spaces.
            private static string ClearString(string str)
            {
                return Regex.Replace(str, @"[^\w\'@-]", " ");
            }
        }
    }
}