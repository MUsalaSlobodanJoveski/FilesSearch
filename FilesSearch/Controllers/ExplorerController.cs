﻿using FilesSearch.Common;
using FilesSearch.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace FilesSearch.Controllers
{
    public class ExplorerController : Controller
    {
        public ActionResult Index(string path)
        {
            string realPath = Server.MapPath(PathConstants.SEARCH_FILES_ROOT + path);

            if (System.IO.File.Exists(realPath))
            {
                // the path is of type file
                return base.File(realPath, PathConstants.CONTENT_OCTET_STREAM);
            }
            else if (Directory.Exists(realPath))
            {
                // search in the folder
                Uri url = Request.Url;
                if (url.ToString().Last() != PathConstants.PATH_BACKSLASH_CHAR)
                {
                    Response.Redirect(PathConstants.EXPLORER_PATH + path + PathConstants.PATH_BACKSLASH);
                }

                Models.ExplorerModel explorerModel = FileOperations.DirFileList(realPath);
                return View(explorerModel);
            }
            else
            {
                // path isn't found
                return Content(path + MessagesConstants.PATH_ISNT_VALID);
            }
        }
    }
}