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

        public int IDuser { get; set; }

        public string coverIMG { get; set; }

        public string img1 { get; set; }

        public string img2 { get; set; }

        public string img3 { get; set; }

        public string img4 { get; set; }

        public string img5 { get; set; }

        public string img6 { get; set; }

        public string img7 { get; set; }

        public string img8 { get; set; }

        public string img9 { get; set; }

        public virtual User User { get; set; }
    }
}
