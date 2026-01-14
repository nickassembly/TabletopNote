using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabletopNote.Shared.Dto;
using TabletopNote.Core.Models;
using TabletopNote.Data;
using TabletopNote.Data.Entities;
using TabletopNote.Shared;

namespace TabletopNote.API.Controllers
{
    [ApiController]
    [Route("campaigns/{campaignId}/documents")]
    public class CampaignDocumentsController : ControllerBase
    {
        private readonly TabletopNoteDbContext _context;

        public CampaignDocumentsController(TabletopNoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET - /campaigns/{campaignId}/documents
        public async Task<ActionResult<DocumentsByCampaignDto>> GetAllCampaignDocuments([FromRoute] int campaignId)
        {
            var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.CampaignId == campaignId);

            if (campaign == null)
                return NotFound($"Campaign {campaignId} does not exist.");

            var campaignDocuments = await _context.CampaignDocuments.Where(cd => cd.CampaignId == campaignId).ToListAsync();

            var campaignDocumentDtos = new List<CampaignDocumentDto>();

            foreach (var campaignDocument in campaignDocuments)
            {
                campaignDocumentDtos.Add(new CampaignDocumentDto
                {
                    DocumentId = campaignDocument.DocumentId,
                    DocumentName = campaignDocument.DocumentName,
                    DocumentDescription = campaignDocument.DocumentDescription,
                    DocumentContentType = (DocumentContentType)campaignDocument.DocumentContentType,
                    DocumentContent = campaignDocument.DocumentContent,
                    IsGMVisibleOnly = campaignDocument.IsGMVisibleOnly,
                    DocumentCreatedAt = campaignDocument.DocumentCreatedAt,
                    DocumentUpdatedAt = campaignDocument.DocumentUpdatedAt,

                });
            }

            var documentsByCampaign = new DocumentsByCampaignDto
            {
                CampaignId = campaignId,
                CampaignName = campaign.CampaignName,
                CampaignDocuments = campaignDocumentDtos
            };

            return documentsByCampaign;
        }

        [HttpGet("{documentId}")]
        // GET - /campaigns/{campaignId}/documents/{documentId}
        public async Task<ActionResult<CampaignDocumentDto>> GetCampaignDocumentById(
            [FromRoute] int campaignId,
            [FromRoute] int documentId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var documentExists = await _context.CampaignDocuments.AnyAsync(d => d.DocumentId == documentId);

            var campaignDocument = await _context.CampaignDocuments
                .FirstOrDefaultAsync(d => d.DocumentId == documentId && d.CampaignId == campaignId);

            if (campaignDocument is null)
                return NotFound($"Document {documentId} for Campaign {campaignId} not found.");

            return Ok(campaignDocument);
        }

        [HttpPost]
        // POST - /campaigns/{campaignId}/documents
        public async Task<ActionResult> AddCampaignDocument(
            [FromRoute] int campaignId,
            [FromBody] CampaignDocumentAddDto newCampaignDocument)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);

            if (!campaignExists)
                return NotFound($"Campaign {campaignId} does not exist.");

            if (newCampaignDocument is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var campaignDocumentToAdd = new CampaignDocumentEntity
            {
                CampaignId = campaignId,
                DocumentName = newCampaignDocument.DocumentName,
                DocumentDescription = newCampaignDocument.DocumentDescription,
                DocumentContentType = (int)newCampaignDocument.DocumentContentType,
                DocumentContent = newCampaignDocument.DocumentContent,
                IsGMVisibleOnly = newCampaignDocument.IsGMVisibleOnly,
                DocumentCreatedAt = DateTime.UtcNow,
                DocumentUpdatedAt = DateTime.UtcNow
            };

            _context.CampaignDocuments.Add(campaignDocumentToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCampaignDocumentById),
                new { campaignId, documentId = campaignDocumentToAdd.DocumentId },
                campaignDocumentToAdd);
        }

        [HttpPut("{documentId}")]
        // PUT - /campaigns/{campaignId}/documents/{documentId}
        public async Task<ActionResult> UpdateCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int documentId,
            [FromBody] CampaignDocumentUpdateDto documentToUpdate)
        {
            if (documentToUpdate is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var documentExists = await _context.CampaignDocuments.AnyAsync(d => d.DocumentId == documentId);

            var campaignDocument = await _context.CampaignDocuments
                .FirstOrDefaultAsync(d => d.DocumentId == documentId && d.CampaignId == campaignId);

            if (campaignDocument is null)
                return NotFound($"Document {documentId} for Campaign {campaignId} not found.");

            campaignDocument.DocumentName = documentToUpdate.DocumentName;
            campaignDocument.DocumentDescription = documentToUpdate.DocumentDescription;
            campaignDocument.DocumentContentType = (int)documentToUpdate.DocumentContentType;
            campaignDocument.DocumentContent = documentToUpdate.DocumentContent;
            campaignDocument.IsGMVisibleOnly = documentToUpdate.IsGMVisibleOnly;
            campaignDocument.DocumentUpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{documentId}")]
        // DELETE - /campaigns/{campaignId}/documents/{documentId}
        public async Task<ActionResult> DeleteCampaignDocument(
             [FromRoute] int campaignId,
             [FromRoute] int documentId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var documentExists = await _context.CampaignDocuments.AnyAsync(d => d.DocumentId == documentId);

            var campaignDocument = await _context.CampaignDocuments
                .FirstOrDefaultAsync(d => d.DocumentId == documentId && d.CampaignId == campaignId);

            if (campaignDocument is null)
                return NotFound($"Document {documentId} for Campaign {campaignId} not found.");

            _context.CampaignDocuments.Remove(campaignDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
