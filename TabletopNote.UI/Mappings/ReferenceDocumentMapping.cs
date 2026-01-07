using TabletopNote.API.Dtos;
using TabletopNote.UI.ViewModels;

namespace TabletopNote.UI.Mappings
{
    public static class ReferenceDocumentMappings
    {
        public static ReferenceDocumentVM ToViewModel(
            this ReferenceDocumentDto dto)
        {
            return new ReferenceDocumentVM
            {
               FileId = dto.FileId,
               ReferenceFileName = dto.ReferenceFileName,
               FileDescription = dto.FileDescription,
               FilePath = dto.FilePath,
               Url = dto.Url,
               IsGMVisibleOnly = dto.IsGMVisibleOnly
            };
        }
    }
}
