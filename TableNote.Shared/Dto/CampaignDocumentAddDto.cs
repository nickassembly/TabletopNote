using System.ComponentModel.DataAnnotations;
using TabletopNote.Core.Models;

namespace TabletopNote.Shared.Dto
{
    public class CampaignDocumentAddDto
    {
        public int CampaignId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Document name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Document name cannot exceed 100 characters.")]
        public string DocumentName { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long.")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string DocumentDescription { get; set; } = string.Empty;
        public DocumentContentType DocumentContentType { get; set; }
        public string? DocumentContent { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}
