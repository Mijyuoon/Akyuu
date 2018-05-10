using Akyuu.Data;
using Akyuu.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Akyuu {
    public partial class App : Application {
        const string ConfigFilePath = "Akyuu.json";

        public Models.Config Config { get; private set; }

        private void App_Startup(object sender, StartupEventArgs e) {
            string exepath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Directory.SetCurrentDirectory(exepath);

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

            if(e.Args.Length > 0) {
                string file = Path.GetFileName(e.Args[0]);

                if(!File.Exists(Path.Combine(Config.ScreenshotPath, file))) {
                    MessageBox.Show($"Invalid filename '{file}' provided, exiting.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(-1);
                }

                var window = new AddTagsWindow(file);
                if(window.ShowDialog() is true) {
                    using(var ctx = AkyuuContext.Create()) {
                        ctx.ScreenshotTags.AddRange(window.Tags);
                        ctx.SaveChanges();
                    }
                }

                Environment.Exit(0);
            }
        }

        private void App_Exit(object sender, ExitEventArgs e) {
            using(var fs = new FileStream(ConfigFilePath, FileMode.Create)) {
                var ser = new DataContractJsonSerializer(typeof(Models.Config));
                ser.WriteObject(fs, Config);
            }
        }
    }
}
