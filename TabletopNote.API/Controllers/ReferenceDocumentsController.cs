using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabletopNote.API.Dtos;
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
        public async Task<ActionResult<List<ReferenceDocumentDto>>> GetAllReferenceDocuments([FromRoute] int campaignId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            if (!campaignExists)
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

            return referenceDocumentDtos;
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

        // TODO - update ReferenceDocument
        [HttpPut]
        // PUT - /campaigns/{campaignId}/references/{fileId}
        public async Task<ActionResult> UpdateCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int fileId,
            [FromBody] ReferenceDocumentUpdateDto referenceToUpdate)
        {
            throw new NotImplementedException();
        }

        // TODO - delete ReferenceDocument
        [HttpDelete]
        // Delete - /campaigns/{campaignId}/references/{fileId}
        public async Task<ActionResult> DeleteCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int fileId)
        {
            throw new NotImplementedException();
        }

    }
}
