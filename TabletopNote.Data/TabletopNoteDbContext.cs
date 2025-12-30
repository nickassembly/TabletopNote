using Microsoft.EntityFrameworkCore;

namespace TabletopNote.Data
{
    public class TabletopNoteDbContext : DbContext
    {
        public TabletopNoteDbContext(DbContextOptions<TabletopNoteDbContext> options)
            : base(options)
        {
        }


    }
}
