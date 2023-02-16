namespace FramesAmorRoma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("booking")]
    public partial class booking
    {
        [Key]
        public int IDbook { get; set; }

        public int IDuser { get; set; }

        public int IDspot { get; set; }

        public DateTime? bookTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime daterequest { get; set; }

        public int prefertHour { get; set; }

        public int IDpackage { get; set; }

        [Required]
        [StringLength(200)]
        public string clientName { get; set; }

        [Required]
        [StringLength(300)]
        public string clientEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string clientTel { get; set; }

        public int NumOfPersons { get; set; }

        public virtual package package { get; set; }

        public virtual spot spot { get; set; }

        public virtual User User { get; set; }
    }
}
