using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Models.DB {
    [Table("ScreenshotTags")]
    public class ScreenshotTag {
        [Key]
        [Column("id", TypeName = "INTEGER")]
        public int ID { get; set; }

        [Column("file", TypeName = "VARCHAR")]
        [Index("ScreenshotTagsIdx", 1, IsUnique = true)]
        public string File { get; set; }

        [Column("tagname", TypeName = "VARCHAR")]
        [Index("ScreenshotTagsIdx", 2, IsUnique = true)]
        public string TagName { get; set; }
    }
}
