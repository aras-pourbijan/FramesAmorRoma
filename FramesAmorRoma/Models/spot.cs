namespace FramesAmorRoma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("spot")]
    public partial class spot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public spot()
        {
            bookings = new HashSet<booking>();
        }

        [Key]
        public int IDspot { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Location") ]
        public string locationName { get; set; }

        [StringLength(1000)]
        public string describtion { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Address") ]
        public string Locationaddress { get; set; }
        

        public string spotIMG { get; set; }

        public string spot1img { get; set; }

        public string spot2img { get; set; }

        public string spot3img { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
    }
}
