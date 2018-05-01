using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }

    public static class Extensions {
        public static bool? ShowDialog(this Window window, Window owner) {
            window.Owner = owner;
            return window.ShowDialog();
        }
    }
}
