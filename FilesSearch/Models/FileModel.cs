using System;

namespace FilesSearch.Models
{
    public class FileModel
    {
        public string FileName { get; set; }

        public string FileSizeText { get; set; }

        public DateTime FileAccessed { get; set; }
    }
}