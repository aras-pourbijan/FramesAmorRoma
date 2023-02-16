namespace FramesAmorRoma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            bookings = new HashSet<booking>();
            portfolios = new HashSet<portfolio>();
        }

        [Key]
        public int IDuser { get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        
        [StringLength(300)]
        [Display (Name ="Emal")]
        public string email { get; set; }

        [Required]
        [StringLength(40)]
        [Display (Name ="Password")]
        public string psw { get; set; }

       
        [StringLength(50)]
        [Display (Name ="First Name")]
        public string FirstName { get; set; }

        
        [StringLength(50)]
        [Display (Name ="Last Name")]
        public string LastName { get; set; }

        public string imgURL { get; set; }

        [Display (Name ="Personal Website")]
        public string Website { get; set; }

        [Display (Name ="Instagram Page")]
        public string instagram { get; set; }

        [StringLength(20)]
        [Display (Name ="cell")]
        public string tel { get; set; }

        [Display (Name ="years of experience")]
        public int? experience { get; set; }

        [StringLength(500)]
        [Display (Name ="About me")]
        public string aboutME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<portfolio> portfolios { get; set; }
    }
}
