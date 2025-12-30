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

        public DbSet<CampaignEntity> Campaigns {  get; set; }
        public DbSet<CampaignDocumentEntity> CampaignDocuments { get; set; }
        public DbSet<ReferenceDocumentEntity> ReferenceDocuments {  get; set; }
        public DbSet<CalendarEventEntity> CalendarEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampaignEntity>(entity =>
            {
                entity.HasKey(e => e.CampaignId);
                entity.ToTable("Campaigns");
            });

            modelBuilder.Entity<CampaignDocumentEntity>(entity =>
            {
                entity.HasKey(e => e.DocumentId);
                entity.ToTable("CampaignDocuments");

                entity.HasOne(d => d.Campaign)
                    .WithMany(c => c.CampaignDocuments)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ReferenceDocumentEntity>(entity =>
            {
                entity.HasKey(e => e.FileId);
                entity.ToTable("ReferenceDocuments");

                entity.HasOne(d => d.Campaign)
                    .WithMany(c => c.ReferenceDocuments)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CalendarEventEntity>(entity =>
            {
                entity.HasKey(e => e.CalendarEventId);
                entity.ToTable("CalendarEvents");

                entity.HasOne(d => d.Campaign)
                    .WithMany(c => c.CalendarEvents)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
