using Microsoft.EntityFrameworkCore;
using TabletopNote.Data.Entities;

namespace TabletopNote.Data
{
    public class TabletopNoteDbContext : DbContext
    {
        public TabletopNoteDbContext(DbContextOptions<TabletopNoteDbContext> options)
            : base(options)
        {
        }

        public DbSet<CampaignEntity> Campaigns => Set<CampaignEntity>();
        public DbSet<CampaignDocumentEntity> CampaignDocuments => Set<CampaignDocumentEntity>();
        public DbSet<ReferenceDocumentEntity> ReferenceDocuments => Set<ReferenceDocumentEntity>();
        public DbSet<CalendarEventEntity> CalendarEvents => Set<CalendarEventEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampaignDocumentEntity>()
                .HasOne(d => d.Campaign)
                .WithMany(c => c.CampaignDocuments)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReferenceDocumentEntity>()
                .HasOne(d => d.Campaign)
                .WithMany(c => c.ReferenceDocuments)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CalendarEventEntity>()
                .HasOne(d => d.Campaign)
                .WithMany(c => c.CalendarEvents)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
