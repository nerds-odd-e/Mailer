namespace Mailer.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MailerDbEntities : DbContext
    {
        public MailerDbEntities()
            : base("name=MailerDbEntities")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
