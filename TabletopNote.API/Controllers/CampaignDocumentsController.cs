using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabletopNote.API.Dtos;
using TabletopNote.Core.Models;
using TabletopNote.Data;
using TabletopNote.Data.Entities;

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
        public async Task<ActionResult<List<CampaignDocumentDto>>> GetAllCampaignDocuments([FromRoute] int campaignId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            if (!campaignExists)
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

            return campaignDocumentDtos;
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
                nameof(AddCampaignDocument),
                new { campaignId, id = campaignDocumentToAdd.DocumentId },
                null);
        }

        // TODO - update CampaignDocument
        [HttpPut]
        // PUT - /campaigns/{campaignId}/documents/{documentId}
        public async Task<ActionResult> UpdateCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int documentId,
            [FromBody] CampaignDocumentUpdateDto documentToUpdate)
        {
            throw new NotImplementedException();
        }

        // TODO - delete CampaignDocument
        [HttpDelete]
        // Delete - /campaigns/{campaignId}/documents/{documentId}
        public async Task<ActionResult> DeleteCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int documentId)
        {
            throw new NotImplementedException();
        }
    }
}
