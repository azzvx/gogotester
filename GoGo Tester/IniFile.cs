using System.Collections.Generic;
using System.Data;
using System.IO;

namespace GoGo_Tester
{
    class IniFile
    {
        public string IniPath
        {
            get;
            set;
        }

        private DataTable _iniTable;
        private List<string> sections;
        public IniFile(string path)
        {
            IniPath = path;

            _iniTable = new DataTable();
            _iniTable.Columns.Add(new DataColumn("Section", typeof(string)));
            _iniTable.Columns.Add(new DataColumn("Key", typeof(string)));
            _iniTable.Columns.Add(new DataColumn("Value", typeof(string)));

            sections = new List<string>();

            var _proxyIniStrings = File.ReadAllLines(path);

            string curSection = "";
            for (int i = 0; i < _proxyIniStrings.Length; i++)
            {
                var str = _proxyIniStrings[i].Trim();
                if (str.Length > 0)
                {
                    if (str.Contains("="))
                    {
                        var kv = str.Split(new[] { '=' });

                        var row = _iniTable.NewRow();
                        row[0] = curSection;
                        row[1] = kv[0].Trim();
                        row[2] = kv[1].Trim();
                        _iniTable.Rows.Add(row);
                    }
                    else if (str.StartsWith("["))
                    {
                        curSection = str;
                        sections.Add(str);
                    }
                    else if (str.StartsWith(";"))
                    {
                        var row = _iniTable.NewRow();
                        row[0] = curSection;
                        row[1] = "";
                        row[2] = str;
                        _iniTable.Rows.Add(row);
                    }
                }
            }
        }

        public void WriteValue(string section, string key, string value)
        {
            var rows = _iniTable.Select(string.Format("Section = '[{0}]' and Key = '{1}'", section, key));
            if (rows.Length > 0)
            {
                rows[0]["Value"] = value;
            }
            else
            {
                var fsection = string.Format("[{0}]", section);

                if (!sections.Contains(fsection))
                    sections.Add(fsection);

                var row = _iniTable.NewRow();
                row["Section"] = fsection;
                row["Key"] = key;
                row["Value"] = value;
                _iniTable.Rows.Add(row);
            }
        }

        public void OverLoad(string path)
        {
            var iniStrings = File.ReadAllLines(path);

            string curSection = "";
            for (int i = 0; i < iniStrings.Length; i++)
            {
                var str = iniStrings[i].Trim();
                if (str.Length > 0)
                {
                    if (str.Contains("="))
                    {
                        var kv = str.Split(new[] { '=' });
                        WriteValue(curSection.Replace("[", "").Replace("]", ""), kv[0].Trim(), kv[1].Trim());
                    }
                    else if (str.StartsWith("["))
                    {
                        curSection = str;

                        if (!sections.Contains(str))
                        {
                            sections.Add(str);
                        }
                    }
                    else if (str.StartsWith(";"))
                    {
                        WriteValue(curSection.Replace("[", "").Replace("]", ""), "", str);
                    }
                }
            }
        }

        public string ReadValue(string section, string key)
        {
            var rows = _iniTable.Select(string.Format("Section = '[{0}]' and Key = '{1}'", section, key));
            if (rows.Length > 0)
            {
                return rows[0][2].ToString();
            }

            return null;
        }

        public void WriteFile(string path = null)
        {
            var strs = new List<string>();

            for (int i = 0; i < sections.Count; i++)
            {
                if (i > 0)
                    strs.Add("\r\n");

                strs.Add(sections[i]);

                var rows = _iniTable.Select(string.Format("Section = '{0}'", sections[i]));

                foreach (var row in rows)
                {
                    if (row[1].ToString() == "")
                    {
                        strs.Add(row[2].ToString());
                    }
                    else
                    {
                        strs.Add(row[1] + " = " + row[2]);
                    }
                }
            }
            File.WriteAllLines(path ?? IniPath, strs.ToArray());
        }

    }
}
