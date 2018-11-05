using System.Collections.Generic;

namespace FilesSearch.Models
{
    public class ExplorerModel
    {
        public List<DirModel> dirModelList;

        public List<FileModel> fileModelList;

        public ExplorerModel(List<DirModel> _dirModelList, List<FileModel> _fileModelList)
        {
            dirModelList = _dirModelList;
            fileModelList = _fileModelList;
        }
    }
}