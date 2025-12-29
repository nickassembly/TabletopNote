using System;
using System.Collections.Generic;
using System.Text;

namespace TabletopNote.Core.Models
{
    public class ReferenceDocument
    {
        public int FileId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileDescription { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string? Url { get; set; }
        public bool IsGMOnly { get; set; }
    }
}
