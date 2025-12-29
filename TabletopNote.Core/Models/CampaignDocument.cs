using System;
using System.Collections.Generic;
using System.Text;

namespace TabletopNote.Core.Models
{
    public class CampaignDocument
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentDescription { get; set; } = string.Empty;
        public DocumentContentType DocumentContentType { get; set; }
        public string? ContentFormat { get; set; }
        public string? DocumentContent { get; set; }
        public bool IsGMOnly { get; set; }
        public DateTime DocumentCreatedAt { get; set; }
        public DateTime DocumentUpdatedAt { get; set; }
    }

    public enum DocumentContentType
    {
        Note,
        Table,
        Other
    }
}
