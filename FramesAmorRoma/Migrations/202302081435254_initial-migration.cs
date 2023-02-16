namespace FramesAmorRoma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.booking",
                c => new
                    {
                        IDbook = c.Int(nullable: false, identity: true),
                        IDuser = c.Int(nullable: false),
                        IDspot = c.Int(nullable: false),
                        bookTime = c.DateTime(),
                        daterequest = c.DateTime(nullable: false, storeType: "date"),
                        prefertHour = c.Int(nullable: false),
                        IDpackage = c.Int(nullable: false),
                        clientName = c.String(nullable: false, maxLength: 200),
                        clientEmail = c.String(nullable: false, maxLength: 300),
                        clientTel = c.String(nullable: false, maxLength: 20, unicode: false),
                        NumOfPersons = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDbook)
                .ForeignKey("dbo.package", t => t.IDpackage)
                .ForeignKey("dbo.spot", t => t.IDspot)
                .ForeignKey("dbo.User", t => t.IDuser)
                .Index(t => t.IDuser)
                .Index(t => t.IDspot)
                .Index(t => t.IDpackage);
            
            CreateTable(
                "dbo.package",
                c => new
                    {
                        IDpackage = c.Int(nullable: false, identity: true),
                        PackageName = c.String(nullable: false, maxLength: 40),
                        PicsIncluded = c.String(nullable: false, maxLength: 15, unicode: false),
                        price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.IDpackage);
            
            CreateTable(
                "dbo.spot",
                c => new
                    {
                        IDspot = c.Int(nullable: false, identity: true),
                        locationName = c.String(nullable: false, maxLength: 100),
                        describtion = c.String(maxLength: 1000),
                        Locationaddress = c.String(nullable: false, maxLength: 200),
                        spotIMG = c.String(),
                        spot1img = c.String(),
                        spot2img = c.String(),
                        spot3img = c.String(),
                    })
                .PrimaryKey(t => t.IDspot);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IDuser = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 300),
                        psw = c.String(nullable: false, maxLength: 40),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        imgURL = c.String(),
                        Website = c.String(),
                        instagram = c.String(),
                        tel = c.String(nullable: false, maxLength: 20, unicode: false),
                        experience = c.Int(),
                        aboutME = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.IDuser);
            
            CreateTable(
                "dbo.portfolio",
                c => new
                    {
                        IDportfolio = c.Int(nullable: false, identity: true),
                        IDuser = c.Int(nullable: false),
                        coverIMG = c.String(),
                        img1 = c.String(),
                        img2 = c.String(),
                        img3 = c.String(),
                        img4 = c.String(),
                        img5 = c.String(),
                        img6 = c.String(),
                        img7 = c.String(),
                        img8 = c.String(),
                        img9 = c.String(),
                    })
                .PrimaryKey(t => t.IDportfolio)
                .ForeignKey("dbo.User", t => t.IDuser)
                .Index(t => t.IDuser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.portfolio", "IDuser", "dbo.User");
            DropForeignKey("dbo.booking", "IDuser", "dbo.User");
            DropForeignKey("dbo.booking", "IDspot", "dbo.spot");
            DropForeignKey("dbo.booking", "IDpackage", "dbo.package");
            DropIndex("dbo.portfolio", new[] { "IDuser" });
            DropIndex("dbo.booking", new[] { "IDpackage" });
            DropIndex("dbo.booking", new[] { "IDspot" });
            DropIndex("dbo.booking", new[] { "IDuser" });
            DropTable("dbo.portfolio");
            DropTable("dbo.User");
            DropTable("dbo.spot");
            DropTable("dbo.package");
            DropTable("dbo.booking");
        }
    }
}
