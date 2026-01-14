using System.ComponentModel.DataAnnotations;

namespace TabletopNote.Shared.Dto
{
    public class ReferenceDocumentUpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "file name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "file name cannot exceed 100 characters.")]
        public string ReferenceFileName { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "File description must be at least 3 characters long.")]
        [MaxLength(2000, ErrorMessage = "File description cannot exceed 2000 characters.")]
        public string FileDescription { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string? Url { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}
