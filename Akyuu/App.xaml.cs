using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Akyuu {
    public partial class App : Application {
        const string ConfigFilePath = "Akyuu.json";

        public Models.Config Config { get; private set; }

        private void App_Startup(object sender, StartupEventArgs e) {
            if(File.Exists(ConfigFilePath)) {
                using(var fs = new FileStream(ConfigFilePath, FileMode.Open)) {
                    var ser = new DataContractJsonSerializer(typeof(Models.Config));
                    Config = ser.ReadObject(fs) as Models.Config;
                }
            } else {
                Config = new Models.Config {
                    // Default config values
                    ScreenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots"),
                    ThumbnailSize = 64,
                };
            }

            Directory.CreateDirectory(Config.ScreenshotPath);
            Data.AkyuuContext.CreateDatabaseIfNotExists();
        }

        private void App_Exit(object sender, ExitEventArgs e) {
            using(var fs = new FileStream(ConfigFilePath, FileMode.Create)) {
                var ser = new DataContractJsonSerializer(typeof(Models.Config));
                ser.WriteObject(fs, Config);
            }
        }
    }
}
