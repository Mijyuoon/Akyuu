using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Data {
    class SQLiteConfiguration : DbConfiguration {
        public SQLiteConfiguration() {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);

            var service = SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices));
            SetProviderServices("System.Data.SQLite", service as DbProviderServices);
        }
    }
}
