namespace FramesAmorRoma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("portfolio")]
    public partial class portfolio
    {
        [Key]
        public int IDportfolio { get; set; }

        [Display (Name ="Photographer")]
        public int IDuser { get; set; }

        [Display (Name ="Cover Image ")]
        public string coverIMG { get; set; }

        [Display (Name ="Image N.1 ")]
        public string img1 { get; set; }

        [Display (Name ="Image N.2 ")]
        public string img2 { get; set; }

        [Display (Name ="Image N.3 ")]
        public string img3 { get; set; }

        [Display (Name ="Image N.4 ")]
        public string img4 { get; set; }

        [Display (Name ="Image N.5 ")]
        public string img5 { get; set; }

        [Display (Name ="Image N.6 ")]
        public string img6 { get; set; }
        [Display (Name ="Image N.7 ")]

        public string img7 { get; set; }
        [Display (Name ="Image N.8 ")]

        public string img8 { get; set; }
        [Display (Name ="Image N.9 ")]

        public string img9 { get; set; }

        public virtual User User { get; set; }
    }
}
