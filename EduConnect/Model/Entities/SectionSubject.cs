using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    [Table("section_subjects")]
    public class SectionSubject
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("section_id")]
        public int SectionId { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        [Column("teacher_id")]
        public int? TeacherId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("SectionId")]
        public Section? Section { get; set; }

        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
