using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Akyuu.Misc {
    public static class Utils {
        private static HashSet<string> imageExtensions =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                ".png", ".jpg", ".jpeg", ".gif", ".bmp",
            };

        public static IEnumerable<string> ListImageFiles(string path) =>
            from t in Directory.EnumerateFiles(path, "*.*")
            where imageExtensions.Contains(Path.GetExtension(t))
            select Path.GetFileName(t);

        public static void ShowInExplorer(string path) {
            path = Path.GetFullPath(path);

            Process.Start(new ProcessStartInfo {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{path}\"",
            });
        }

        public static void OpenFileDefault(string path) {
            path = Path.GetFullPath(path);

            Process.Start(new ProcessStartInfo {
                FileName = path,
                UseShellExecute = true,
            });
        }

        public static void ImageFileToClipboard(string path) {
            path = Path.GetFullPath(path);

            var uri = new Uri(path, UriKind.Absolute);
            Clipboard.SetImage(new BitmapImage(uri));
        }
    }

    public static class Extensions {
        public static bool? ShowDialog(this Window window, Window owner) {
            window.Owner = owner;
            return window.ShowDialog();
        }

        public static bool HasModifier(this KeyEventArgs e, ModifierKeys modifier) {
            return (e.KeyboardDevice.Modifiers & modifier) == modifier;
        }

        public static bool ContainsCI(this string source, string value) {
            var cinfo = CultureInfo.InvariantCulture.CompareInfo;
            return cinfo.IndexOf(source, value, CompareOptions.IgnoreCase) > -1;
        }
    }
}
