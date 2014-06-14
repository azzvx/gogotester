using System.Collections.Generic;
using System.Data;
using System.IO;

namespace GoGo_Tester_NET4._0
{
    class IniFile
    {
        public string IniPath
        {
            get;
            set;
        }

        private DataTable _iniTable;

        public IniFile(string path)
        {

            IniPath = path;


            _iniTable = new DataTable();
            _iniTable.Columns.Add(new DataColumn("Section", typeof(string)));
            _iniTable.Columns.Add(new DataColumn("Key", typeof(string)));
            _iniTable.Columns.Add(new DataColumn("Value", typeof(string)));


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


        /// <summary>  
        /// 写ini文件  
        /// </summary>  
        /// <param name="section">Section</param>  
        /// <param name="key">Key</param>  
        /// <param name="value">Value</param>  
        public void WriteValue(string section, string key, string value)
        {
            var rows = _iniTable.Select(string.Format("Section = '[{0}]' and Key = '{1}'", section, key));
            if (rows.Length > 0)
            {
                rows[0][2] = value;
            }
        }

        /// <summary>  
        /// 读取ini节点的键值  
        /// </summary>  
        /// <param name="section">Section</param>  
        /// <param name="key">Key</param>  
        /// <returns>string</returns>  
        public string ReadValue(string section, string key)
        {
            var rows = _iniTable.Select(string.Format("Section = '[{0}]' and Key = '{1}'", section, key));
            if (rows.Length > 0)
            {
                return rows[0][2].ToString();
            }

            return null;
        }

        public void WriteFile()
        {
            string curSection = "";
            var strs = new List<string>();

            for (int i = 0; i < _iniTable.Rows.Count; i++)
            {
                var row = _iniTable.Rows[i];

                if (row[0].ToString() != curSection)
                {
                    if (curSection != "")
                        strs.Add("\r\n");

                    curSection = row[0].ToString();
                    strs.Add(curSection);
                }

                if (row[1].ToString() == "")
                {
                    strs.Add(row[2].ToString());
                }
                else
                {
                    strs.Add(row[1] + " = " + row[2]);
                }
            }

            File.WriteAllLines(IniPath, strs.ToArray());
        }

    }
}
