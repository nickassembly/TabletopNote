namespace TabletopNote.Shared.Dto
{
    public class ReferenceDocumentDto
    {
        public int CampaignId { get; set; }
        public int FileId { get; set; }
        public string ReferenceFileName { get; set; } = string.Empty;
        public string FileDescription { get; set; } = string.Empty;
        public string? FilePath { get; set; }

        // TODO - Update Reference Document properties to put URL as part of an Enum with type (URL | Uploaded File)
        // see DocumentContentType in CampaignDocumentAddDto.cs
        public string? Url { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}

