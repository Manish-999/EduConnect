using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Collections.Specialized.BitVector32;

namespace Model.Entities
{
    [Table("classes")]
    public class Class
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("school_id")]
        public int SchoolId { get; set; }

        [Column("class_name")]
        public string ClassName { get; set; } = string.Empty;
        [Column("class_teacher")]
        public int? ClassTeacher { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("SchoolId")]
        public School? School { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
