using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    [Table("sections")]
    public class Section
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("class_id")]
        public int ClassId { get; set; }

        [Column("section_name")]
        public string SectionName { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<SectionSubject> SectionSubjects { get; set; } = new List<SectionSubject>();
    }
}
