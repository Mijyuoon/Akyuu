using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Data {
    class SQLiteConfiguration : DbConfiguration {
        const string ModelCachePath = "EFCache";

        public SQLiteConfiguration() {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);

            var service = SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices));
            SetProviderServices("System.Data.SQLite", service as DbProviderServices);

            Directory.CreateDirectory(ModelCachePath);

            var cachedModelStore = new SQLiteModelStore(ModelCachePath);
            var depResolver = new SingletonDependencyResolver<DbModelStore>(cachedModelStore);
            AddDependencyResolver(depResolver);
        }

        private class SQLiteModelStore : DefaultDbModelStore {
            public SQLiteModelStore(string location) : base(location) { }

            public override DbCompiledModel TryLoad(Type contextType) {
                string path = GetFilePath(contextType);
                if(File.Exists(path)) {
                    DateTime mdlTime = File.GetLastWriteTimeUtc(path);

                    string asmPath = typeof(App).Assembly.Location;
                    DateTime asmTime = File.GetLastWriteTimeUtc(asmPath);

                    if(asmTime > mdlTime) {
                        File.Delete(path);
                        Debug.WriteLine("Cached DB model obsolete. Re-creating cached DB model edmx.");
                    }
                } else {
                    Debug.WriteLine("No cached DB model found. Creating cached DB model edmx.");
                }

                return base.TryLoad(contextType);
            }
        }
    }
}
