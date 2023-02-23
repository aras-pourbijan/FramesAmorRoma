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

        [Display(Name = "choose your favorite location")]
        public int IDspot { get; set; }
       
        public  DateTime  bookTime { get; set; }


        [Column(TypeName = "date")]
        [Display(Name = "select a date")]

        public DateTime daterequest { get; set; }
        
        [Display(Name = "your preferred time")]
        [DataType(DataType.Time)]
        public DateTime prefertHour { get; set; }

        [Display(Name = "choose one of the packages")]
        public int IDpackage { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Your Name")]
        public string clientName { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Your Email")]
        public string clientEmail { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "your mobile number")]
        public string clientTel { get; set; }

        [Display(Name = "how many people are you?")]
        [Range(1,10, ErrorMessage =" choose a number between 1 and 10, to add more person ask it than your photographer ")]
        public int NumOfPersons { get; set; }
      

        public virtual package package { get; set; }

        public virtual spot spot { get; set; }

        public virtual User User { get; set; }
    }
    
}
