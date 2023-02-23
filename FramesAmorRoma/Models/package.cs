namespace FramesAmorRoma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("package")]
    public partial class package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public package()
        {
            bookings = new HashSet<booking>();
        }

        [Key]
        [Display(Name = "desired package")]
        public int IDpackage { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name ="Package")]
        public string PackageName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name ="Photos included in package")]
        public string PicsIncluded { get; set; }
        [Column(TypeName = "money")]
        public decimal price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
    }
}
