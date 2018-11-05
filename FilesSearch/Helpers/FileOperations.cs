using FilesSearch.Common;
using FilesSearch.Models;
using System.Collections.Generic;
using System.IO;

namespace FilesSearch.Helpers
{
    public static class FileOperations
    {
        public static ExplorerModel DirFileList(string realPath)
        {
            List<FileModel> fileListModel = new List<FileModel>();
            List<DirModel> dirListModel = new List<DirModel>();
            IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            foreach (string dir in dirList)
            {
                DirectoryInfo d = new DirectoryInfo(dir);
                DirModel dirModel = new DirModel
                {
                    DirName = Path.GetFileName(dir),
                    DirAccessed = d.LastAccessTime
                };
                dirListModel.Add(dirModel);
            }

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);
                FileModel fileModel = new FileModel
                {
                    FileName = Path.GetFileName(file),
                    FileAccessed = f.LastAccessTime,
                    FileSizeText = (f.Length < 1024) ? f.Length.ToString() + CommonConstants.BYTE : f.Length / 1024 + CommonConstants.KILOBYTE
                };
                fileListModel.Add(fileModel);
            }

            return new ExplorerModel(dirListModel, fileListModel);
        }
    }
}