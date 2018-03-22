namespace Mailer.Models
{
    using System.Data.Entity;

    public partial class MailerDbEntities : DbContext
    {
        public MailerDbEntities()
            : base("name=MailerDbEntities")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
