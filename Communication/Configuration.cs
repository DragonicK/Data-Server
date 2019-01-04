using System;
using System.IO;
using Data_Server.Util;

namespace Data_Server.Communication {
    public static class Configuration {
        public static int MaxPlayerImprovement { get; set; }

        public static string Server { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Database { get; set; }
        public static int Port { get; set; }
        public static int MinPoolSize { get; set; }
        public static int MaxPoolSize { get; set; }

        public static void ReadSqlConfig() {
            string file = "./MySQL.ini";

            if (File.Exists(file)) {
                Server = Settings.GetValue("DATA", "Server", file);
                Username = Settings.GetValue("DATA", "Username", file);
                Password = Settings.GetValue("DATA", "Password", file);
                Database = Settings.GetValue("DATA", "Database", file);
                Port = Convert.ToInt32(Settings.GetValue("DATA", "Port", file));
                MinPoolSize = Convert.ToInt32(Settings.GetValue("DATA", "MinPoolSize", file));
                MaxPoolSize = Convert.ToInt32(Settings.GetValue("DATA", "MaxPoolSize", file));
            }
            else {
                CreateFileConfig(file);
                ReadSqlConfig();
            }
        }

        public static void CreateFileConfig(string file) {
            Settings.SetValue("DATA", "Server", file, "127.0.0.1");
            Settings.SetValue("DATA", "Username", file, "root");
            Settings.SetValue("DATA", "Password", file, "root");
            Settings.SetValue("DATA", "Database", file, "crystalshire");
            Settings.SetValue("DATA", "Port", file, "3306");
            Settings.SetValue("DATA", "MinPoolSize", file, "5");
            Settings.SetValue("DATA", "MaxPoolSize", file, "100");
        }
    }
}