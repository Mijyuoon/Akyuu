using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows;

namespace Akyuu.Models {
    [DataContract]
    public class Config : BaseModel {
        [DataMember(Name = nameof(ScreenshotPath))]
        private string _ScreenshotPath;
        public string ScreenshotPath {
            get { return _ScreenshotPath; }
            set { SetProperty(ref _ScreenshotPath, value, nameof(ScreenshotPath)); }
        }

        [DataMember(Name = nameof(ThumbnailSize))]
        private int _ThumbnailSize;
        public int ThumbnailSize {
            get { return _ThumbnailSize; }
            set { SetProperty(ref _ThumbnailSize, value, nameof(ThumbnailSize)); }
        }

        public static Config Current => (Application.Current as App).Config;
    }
}
