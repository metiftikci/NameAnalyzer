// Copyright (c) 2020 jaqra. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameAnalyzer.Internal
{
    internal class SettingsFileReader
    {
        private readonly string _path;

        public SettingsFileReader(string path)
        {
            _path = path;
        }

        public NamingRule[] Parse()
        {
            if (!File.Exists(_path)) return new NamingRule[] { };

            string[] lines = File.ReadAllLines(_path);

            List<NamingRule> rules = new List<NamingRule>();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(';').Select(x => x.Trim()).ToArray();

                if (parts.Length != 2) continue;

                NamingRule rule = new NamingRule()
                {
                    LocationFormat = parts[0],
                    NameFormat = parts[1]
                };

                if (!rule.IsValid()) continue;

                rule.NormalizeFormats();

                rules.Add(rule);
            }

            return rules.ToArray();
        }
    }
}
