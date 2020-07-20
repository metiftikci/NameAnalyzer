// Copyright (c) 2020 jaqra. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.RegularExpressions;

namespace NameAnalyzer.Internal
{
    internal static class RegexHelper
    {
        public static bool IsPatternValid(string pattern)
        {
            try
            {
                new Regex(pattern);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
