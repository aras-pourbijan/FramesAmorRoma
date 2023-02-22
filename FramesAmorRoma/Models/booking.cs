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
       
        public  DateTime  bookTime { get; set; }


        [Column(TypeName = "date")]
        [Display(Name = "Date Requested")]

        public DateTime daterequest { get; set; }
        
        [Display(Name = "Hour Preferences")]
        [DataType(DataType.Time)]
        public DateTime prefertHour { get; set; }

        [Display(Name = "Choose Your Package")]
        public int IDpackage { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Client full Name")]
        public string clientName { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "client Email")]
        public string clientEmail { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "client contact number")]
        public string clientTel { get; set; }

        [Display(Name = "Number of persons for shooting ")]
        public int NumOfPersons { get; set; }

        public virtual package package { get; set; }

        public virtual spot spot { get; set; }

        public virtual User User { get; set; }
    }
    
}
