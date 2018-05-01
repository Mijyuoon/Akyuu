using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Models {
    public class Screenshot : BaseModel {
        private Uri _Source;
        public Uri Source {
            get { return _Source; }
            set { SetProperty(ref _Source, value, nameof(Source), nameof(File)); }
        }

        public string File => Path.GetFileName(Source.AbsolutePath);

        public static Screenshot FromPath(string path) {
            return new Screenshot { Source = new Uri(path, UriKind.Absolute) };
        }

        public static Screenshot FromFile(string file) {
            return FromPath(Path.Combine(Config.Current.ScreenshotPath, file));
        }
    }
}
