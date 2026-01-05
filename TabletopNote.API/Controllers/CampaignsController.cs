using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabletopNote.API.Dtos;
using TabletopNote.Core.Models;
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

        // TODO ** All controllers need to wrap all requests in Try-Catch
        // Possibly include an error handling middleware layer, or use generic error handling for now

        [HttpGet]
        // GET - /campaigns
        public async Task<ActionResult<List<CampaignDto>>> GetAllCampaigns()
        {
            var campaigns = await _context.Campaigns
                           .Include(c => c.CampaignDocuments)
                           .Include(c => c.CalendarEvents)
                           .Include(c => c.ReferenceDocuments)
                           .ToListAsync();

            var allCampaigns = new List<CampaignDto>();

            foreach (var campaign in campaigns)
            {
                var campaignDto = new CampaignDto
                {
                    CampaignId = campaign.CampaignId,
                    CampaignName = campaign.CampaignName,
                    CampaignDescription = campaign.CampaignDescription,
                    CampaignDocuments = campaign.CampaignDocuments
                    .Select(d => new CampaignDocumentDto
                    {
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        DocumentDescription = d.DocumentDescription,
                        DocumentContentType = (DocumentContentType)d.DocumentContentType,
                        DocumentContent = d.DocumentContent,
                        IsGMVisibleOnly = d.IsGMVisibleOnly,
                        DocumentCreatedAt = d.DocumentCreatedAt,
                        DocumentUpdatedAt = d.DocumentUpdatedAt
                    }).ToList(),
                    ReferenceDocuments = campaign.ReferenceDocuments
                    .Select(r => new ReferenceDocumentDto
                    {
                        FileId = r.FileId,
                        ReferenceFileName = r.ReferenceFileName,
                        FileDescription = r.FileDescription,
                        FilePath = r.FilePath,
                        Url = r.Url,
                        IsGMVisibleOnly = r.IsGMVisibleOnly
                    }).ToList(),
                    CalendarEvents = campaign.CalendarEvents
                    .Select(e => new CalendarEventDto
                    {
                        CalendarEventId = e.CalendarEventId,
                        EventName = e.EventName,
                        EventDescription = e.EventDescription,
                        EventStartDate = e.EventStartDate,
                        EventEndDate = e.EventEndDate,
                        IsGMVisibleOnly = e.IsGMVisibleOnly
                    }).ToList()
                };

                allCampaigns.Add(campaignDto);
            }

            return allCampaigns;
        }

        [HttpGet("{id}")]
        // GET - /campaigns/{id}
        public async Task<ActionResult<CampaignDto>> GetCampaignById(int id)
        {
            var campaign = await _context.Campaigns
                        .Include(c => c.CampaignDocuments)
                        .Include(c => c.CalendarEvents)
                        .Include(c => c.ReferenceDocuments)
                        .FirstOrDefaultAsync(c => c.CampaignId == id);

            if (campaign == null)
                return NotFound();

            var campaignDto = new CampaignDto
            {
                CampaignId = campaign.CampaignId,
                CampaignName = campaign.CampaignName,
                CampaignDescription = campaign.CampaignDescription,
                CampaignDocuments = campaign.CampaignDocuments
                    .Select(d => new CampaignDocumentDto
                    {
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        DocumentDescription = d.DocumentDescription,
                        DocumentContentType = (DocumentContentType)d.DocumentContentType,
                        DocumentContent = d.DocumentContent,
                        IsGMVisibleOnly = d.IsGMVisibleOnly,
                        DocumentCreatedAt = d.DocumentCreatedAt,
                        DocumentUpdatedAt = d.DocumentUpdatedAt
                    }).ToList(),
                ReferenceDocuments = campaign.ReferenceDocuments
                    .Select(r => new ReferenceDocumentDto
                    {
                        FileId = r.FileId,
                        ReferenceFileName = r.ReferenceFileName,
                        FileDescription = r.FileDescription,
                        FilePath = r.FilePath,
                        Url = r.Url,
                        IsGMVisibleOnly = r.IsGMVisibleOnly
                    }).ToList(),
                CalendarEvents = campaign.CalendarEvents
                    .Select(e => new CalendarEventDto
                    {
                        CalendarEventId = e.CalendarEventId,
                        EventName = e.EventName,
                        EventDescription = e.EventDescription,
                        EventStartDate = e.EventStartDate,
                        EventEndDate = e.EventEndDate,
                        IsGMVisibleOnly = e.IsGMVisibleOnly
                    }).ToList()
            };

            return campaignDto;
        }

        [HttpPost]
        // POST - /campaigns
        public async Task<ActionResult> AddCampaign([FromBody] CampaignAddDto? newCampaign)
        {
            if (newCampaign is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var campaignToAdd = new CampaignEntity
            {
                CampaignName = newCampaign.CampaignName,
                CampaignDescription = newCampaign.CampaignDescription
            };

            _context.Campaigns.Add(campaignToAdd);
            await _context.SaveChangesAsync();

            var response = new CampaignDto
            {
                CampaignId = campaignToAdd.CampaignId,
                CampaignName = campaignToAdd.CampaignName,
                CampaignDescription = campaignToAdd.CampaignDescription,
                CampaignDocuments = new List<CampaignDocumentDto>(),
                ReferenceDocuments = new List<ReferenceDocumentDto>(),
                CalendarEvents = new List<CalendarEventDto>()
            };

            return CreatedAtAction(nameof(GetCampaignById), new { id = response.CampaignId }, response);
        }

        [HttpPut("{id}")]
        // PUT - /campaigns/{id}
        public async Task<ActionResult> UpdateCampaign(
            [FromBody] CampaignUpdateDto? campaignToUpdate, 
            [FromRoute] int id)
        {
            var campaign = await _context.Campaigns
                        .Include(c => c.CampaignDocuments)
                        .Include(c => c.CalendarEvents)
                        .Include(c => c.ReferenceDocuments)
                        .FirstOrDefaultAsync(c => c.CampaignId == id);

            if (campaign == null)
                return NotFound();

            if (campaignToUpdate is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            campaign.CampaignName = campaignToUpdate.CampaignName;
            campaign.CampaignDescription = campaignToUpdate.CampaignDescription;

            _context.Campaigns.Update(campaign);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        // DELETE - /campaigns/{id}
        public async Task<ActionResult> DeleteCampaign(
           [FromRoute] int id)
        {
            var campaign = await _context.Campaigns
                        .Include(c => c.CampaignDocuments)
                        .Include(c => c.CalendarEvents)
                        .Include(c => c.ReferenceDocuments)
                        .FirstOrDefaultAsync(c => c.CampaignId == id);

            if (campaign == null)
                return NotFound();

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
