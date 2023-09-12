using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Utils
{
    public static class StringExtensionMethods
    {
        private static readonly Dictionary<char, char> _diacriticMap = new Dictionary<char, char>
        {
            {'ą', 'a'},
            {'ć', 'c'},
            {'ę', 'e'},
            {'ł', 'l'},
            {'ń', 'n'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ź', 'z'},
            {'ż', 'z'}
        };
        
        public static bool CompareNormalizedStrings(string str1, string str2)
        {
            var normalizedStr1 = NormalizeString(str1.ToLower());
            var normalizedStr2 = NormalizeString(str2.ToLower());

            return normalizedStr1.Equals(normalizedStr2);
        }

        public static string NormalizeString(string text)
        {
            var normalizedText = new StringBuilder();
        
            foreach (var c in text)
            {
                if (_diacriticMap.ContainsKey(c))
                {
                    normalizedText.Append(_diacriticMap[c]);
                }
                else
                {
                    normalizedText.Append(c);
                }
            }

            return normalizedText.ToString();
        }

        public static string GetStringTillChar(string inputText, string delimiter)
        {
            var delimiterIndex = inputText.IndexOf(delimiter, StringComparison.Ordinal);
            var extractedText = inputText[..delimiterIndex];
            return extractedText;
        }
    }
}