using System.ComponentModel.DataAnnotations;
using TabletopNote.Core.Models;

namespace TabletopNote.Shared.Dto
{
    public class CampaignUpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Campaign name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Campaign name cannot exceed 100 characters.")]
        public string CampaignName { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long.")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string CampaignDescription { get; set; } = string.Empty;
    }
}
