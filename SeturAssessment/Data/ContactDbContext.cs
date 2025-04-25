using ContactService.Models;
using Microsoft.EntityFrameworkCore;

namespace SeturAssessment.Data
{
    public class ContactDbContext :DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }

        public DbSet<ContactInfoModel> ContactInfoModel { get; set; }
        public DbSet<ContactModel> ContactModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactModel>()
                .HasMany(c => c.Infos)
                .WithOne(i => i.Contact)
                .HasForeignKey(i => i.ContactId);

            modelBuilder.Entity<ContactInfoModel>()
                .HasOne(i => i.Contact)
                .WithMany(c => c.Infos)
                .HasForeignKey(i => i.ContactId);
        }
    }
}
