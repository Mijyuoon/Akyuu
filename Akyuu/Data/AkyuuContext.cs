using Akyuu.Models;
using Akyuu.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Data {
    public class AkyuuContext : DbContext {
        public static readonly string DbName = "AkyuuData.db";

        private AkyuuContext() : base(new SQLiteConnection {
            ConnectionString = MakeConnectionString(),
        }, true) {
            // Idk
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public static string MakeConnectionString() {
            var conn = new SQLiteConnectionStringBuilder() {
                DataSource = Path.Combine(Config.Current.ScreenshotPath, DbName),
                ForeignKeys = true,
            };

            return conn.ConnectionString;
        }

        public static AkyuuContext Create() {
            Directory.CreateDirectory(Config.Current.ScreenshotPath);

            var context = new AkyuuContext();

            string path = Path.Combine(Config.Current.ScreenshotPath, DbName);
            if(!File.Exists(path)) {
                context.Database.ExecuteSqlCommand(Properties.Resources.DatabaseSchema);
            }

            return context;
        }

        public DbSet<ScreenshotTag> ScreenshotTags { get; set; }
    }
}
