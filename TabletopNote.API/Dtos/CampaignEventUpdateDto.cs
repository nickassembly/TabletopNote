using System.ComponentModel.DataAnnotations;

namespace TabletopNote.API.Dtos
{
    public class CampaignEventUpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Event name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string EventName { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Event description must be at least 3 characters long.")]
        [MaxLength(2000, ErrorMessage = "Event description cannot exceed 2000 characters.")]
        public string EventDescription { get; set; } = string.Empty;
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}
