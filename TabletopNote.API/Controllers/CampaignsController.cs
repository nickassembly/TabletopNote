using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabletopNote.Data;
using TabletopNote.Data.Entities;

namespace TabletopNote.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly TabletopNoteDbContext _context;

        public CampaignsController(TabletopNoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CampaignEntity>>> GetCampaigns()
        {
            return await _context.Campaigns.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignEntity>> GetCampaignById(int id)
        {
            var campaign = await _context.Campaigns
                        .Include(c => c.CampaignDocuments)
                        .Include(c => c.CalendarEvents)
                        .Include(c => c.ReferenceDocuments)
                        .FirstOrDefaultAsync(c => c.CampaignId == id);

            if (campaign == null)
                return NotFound();

            return campaign;
        }

    }
}
