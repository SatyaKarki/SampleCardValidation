namespace Card.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CardContext : DbContext
    {
        public CardContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<CardNumber> CardNumbers { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardType>()
                .HasMany(e => e.CardNumbers)
                .WithRequired(e => e.CardType)
                .WillCascadeOnDelete(false);
        }
    }
}
