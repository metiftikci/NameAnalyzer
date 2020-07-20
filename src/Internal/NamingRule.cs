// Copyright (c) 2020 jaqra. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace NameAnalyzer.Internal
{
    internal class NamingRule
    {
        public string LocationFormat { get; set; }
        public string NameFormat { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(LocationFormat)) return false;
            if (string.IsNullOrWhiteSpace(NameFormat)) return false;
            if (!RegexHelper.IsPatternValid(LocationFormat)) return false;
            if (!RegexHelper.IsPatternValid(NameFormat)) return false;

            return true;
        }

        public void NormalizeFormats()
        {
            LocationFormat = NormalizeFormat(LocationFormat);
            NameFormat = NormalizeFormat(NameFormat);
        }

        private string NormalizeFormat(string format)
        {
            if (format[0] != '^') format = '^' + format;
            if (format[format.Length - 1] != '$') format += '$';

            return format;
        }
    }
}
