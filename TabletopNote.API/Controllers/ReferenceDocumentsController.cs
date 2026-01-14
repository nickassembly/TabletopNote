using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabletopNote.Shared.Dto;
using TabletopNote.Core.Models;
using TabletopNote.Data;
using TabletopNote.Data.Entities;

namespace TabletopNote.API.Controllers
{
    [ApiController]
    [Route("campaigns/{campaignId}/references")]
    public class ReferenceDocumentsController : ControllerBase
    {
        private readonly TabletopNoteDbContext _context;

        public ReferenceDocumentsController(TabletopNoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET - /campaigns/{campaignId}/references
        public async Task<ActionResult<ReferencesByCampaignDto>> GetAllReferenceDocuments([FromRoute] int campaignId)
        {
            var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.CampaignId == campaignId);

            if (campaign == null)
                return NotFound($"Campaign {campaignId} does not exist.");

            var referenceDocuments = await _context.ReferenceDocuments.Where(cd => cd.CampaignId == campaignId).ToListAsync();

            var referenceDocumentDtos = new List<ReferenceDocumentDto>();

            foreach (var referenceDocument in referenceDocuments)
            {
                referenceDocumentDtos.Add(new ReferenceDocumentDto
                {
                    FileId = referenceDocument.FileId,
                    ReferenceFileName = referenceDocument.ReferenceFileName,
                    FileDescription = referenceDocument.FileDescription,
                    FilePath = referenceDocument.FilePath,
                    Url = referenceDocument.Url,
                    IsGMVisibleOnly = referenceDocument.IsGMVisibleOnly
                });
            }

            var referenceByCampaign = new ReferencesByCampaignDto
            {
                CampaignId = campaignId,
                CampaignName = campaign.CampaignName,
                ReferenceDocuments = referenceDocumentDtos
            };

            return referenceByCampaign;
        }

        [HttpGet("{fileId}")]
        // GET - /campaigns/{campaignId}/references/{fileId}
        public async Task<ActionResult<CampaignDocumentDto>> GetReferenceDocumentById(
            [FromRoute] int campaignId,
            [FromRoute] int fileId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var documentExists = await _context.ReferenceDocuments.AnyAsync(r => r.FileId == fileId);

            var referenceDocument = await _context.ReferenceDocuments
                .FirstOrDefaultAsync(r => r.FileId == fileId && r.CampaignId == campaignId);

            if (referenceDocument is null)
                return NotFound($"File Id {fileId} for Campaign {campaignId} not found.");

            return Ok(referenceDocument);
        }

        [HttpPost]
        // POST - /campaigns/{campaignId}/references
        public async Task<ActionResult> AddReferenceDocument(
            [FromRoute] int campaignId,
            [FromBody] ReferenceDocumentAddDto newReferenceDocument)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);

            if (!campaignExists)
                return NotFound($"Campaign {campaignId} does not exist.");

            if (newReferenceDocument is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var referenceDocumentToAdd = new ReferenceDocumentEntity
            {
                CampaignId = campaignId,
                ReferenceFileName = newReferenceDocument.ReferenceFileName,
                FileDescription = newReferenceDocument.FileDescription,
                FilePath = newReferenceDocument.FilePath,
                Url = newReferenceDocument.Url,
                IsGMVisibleOnly = newReferenceDocument.IsGMVisibleOnly
            };

            _context.ReferenceDocuments.Add(referenceDocumentToAdd);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(AddReferenceDocument),
                new { campaignId, id = referenceDocumentToAdd.FileId },
                null
            );
        }

        [HttpPut("{fileId}")]
        // PUT - /campaigns/{campaignId}/references/{fileId}
        public async Task<ActionResult> UpdateReferenceDocument(
            [FromRoute] int campaignId,
            [FromRoute] int fileId,
            [FromBody] ReferenceDocumentUpdateDto referenceToUpdate)
        {
            if (referenceToUpdate is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var referenceExists = await _context.ReferenceDocuments.AnyAsync(r => r.FileId == fileId);

            var referenceDocument = await _context.ReferenceDocuments
                .FirstOrDefaultAsync(r => r.FileId == fileId && r.CampaignId == campaignId);

            if (referenceDocument is null)
                return NotFound($"Reference {fileId} for Campaign {campaignId} not found.");

            referenceDocument.ReferenceFileName = referenceToUpdate.ReferenceFileName;
            referenceDocument.FileDescription = referenceToUpdate.FileDescription;
            referenceDocument.FilePath = referenceToUpdate.FilePath;
            referenceDocument.Url = referenceToUpdate.Url;
            referenceDocument.IsGMVisibleOnly = referenceToUpdate.IsGMVisibleOnly;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{fileId}")]
        // DELETE - /campaigns/{campaignId}/references/{fileId}
        public async Task<ActionResult> DeleteReferenceDocument(
             [FromRoute] int campaignId,
             [FromRoute] int fileId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var referenceExists = await _context.ReferenceDocuments.AnyAsync(r => r.FileId == fileId);

            var referenceDocument = await _context.ReferenceDocuments
                .FirstOrDefaultAsync(r => r.FileId == fileId && r.CampaignId == campaignId);

            if (referenceDocument is null)
                return NotFound($"Reference {fileId} for Campaign {campaignId} not found.");

            _context.ReferenceDocuments.Remove(referenceDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
