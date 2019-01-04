using System.Text;
using System.Runtime.InteropServices;

namespace Data_Server.Util {
    public static class Settings {
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public static string GetValue(string section, string key, string fileName) {
            int chars = 1000;
            StringBuilder buffer = new StringBuilder(chars);

            string sDefault = string.Empty;
            if (GetPrivateProfileString(section, key, sDefault, buffer, chars, fileName) != 0) {
                return buffer.ToString();
            }
            else {
                return sDefault;
            }
        }

        public static void SetValue(string section, string key, string fileName, object value) {
            WritePrivateProfileString(section, key, (string)value, fileName);
        }
    }
}