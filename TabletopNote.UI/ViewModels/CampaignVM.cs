using System.ComponentModel.DataAnnotations;
using TabletopNote.Shared.Dto;

namespace TabletopNote.UI.ViewModels
{
    public class CampaignVM
    {
        public int CampaignId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Event name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string CampaignName { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long.")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string? CampaignDescription { get; set; }
        public List<CampaignDocumentDto> CampaignDocuments { get; set; } = new();
        public List<ReferenceDocumentDto> ReferenceDocuments { get; set; } = new();
        public List<CalendarEventDto> CalendarEvents { get; set; } = new();
    }
}
