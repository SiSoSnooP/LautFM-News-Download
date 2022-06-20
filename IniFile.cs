using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Laut_News
{
    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,string key, string def, StringBuilder retVal,int size, string filePath);
        public IniFile(string Path)
        {
            path = Path;

            if (!File.Exists(path))
            {
                File.Create(path);
            }

        }
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
           if (temp.ToString() == "")
            {
                this.IniWriteValue(Section, Key,"");

            }

            return temp.ToString();

        }
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }
    }
}
