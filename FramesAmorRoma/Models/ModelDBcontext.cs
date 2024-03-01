using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FramesAmorRoma.Models
{
    public partial class ModelDBcontext : DbContext
    {
        public ModelDBcontext()
            : base("name=PhotographerDB")
        {
        }

        public virtual DbSet<booking> Bookings { get; set; }
        public virtual DbSet<package> Packages { get; set; }
        public virtual DbSet<portfolio> Portfolios { get; set; }
        public virtual DbSet<spot> Spots { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<booking>()
                .Property(e => e.clientTel)
                .IsUnicode(false);

            modelBuilder.Entity<package>()
                .Property(e => e.PicsIncluded)
                .IsUnicode(false);

            modelBuilder.Entity<package>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<package>()
                .HasMany(e => e.bookings)
                .WithRequired(e => e.package)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<spot>()
                .HasMany(e => e.bookings)
                .WithRequired(e => e.spot)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.tel)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.bookings)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.portfolios)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
