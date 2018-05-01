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

        public AkyuuContext() : base(new SQLiteConnection {
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

        public static void CreateDatabaseIfNotExists() {
            string path = Path.Combine(Config.Current.ScreenshotPath, DbName);
            if(File.Exists(path)) return;

            using(var ctx = new AkyuuContext()) {
                ctx.Database.ExecuteSqlCommand(Properties.Resources.DatabaseSchema);
            }
        }

        public DbSet<ScreenshotTag> ScreenshotTags { get; set; }
    }
}
