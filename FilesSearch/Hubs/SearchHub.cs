﻿using FilesSearch.Common;
using FilesSearch.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace FilesSearch.Hubs
{
    public class SearchHub : Hub
    {
        public void Send(string dirPath, string searchItem)
        {
            dirPath = HttpUtility.UrlDecode(dirPath);
            IEnumerable<string> fileList = Directory.EnumerateFiles(dirPath);
            List<SignalRResult> results = new List<SignalRResult>
            {
                new SignalRResult { ItemValue = MessagesConstants.SEARCHING_FOR + searchItem}
            };

            FilesSearch(dirPath, searchItem, fileList, ref results);

            Clients.All.broadcastMessage(dirPath, JsonConvert.SerializeObject(results));
        }

        private void FilesSearch(string dirPath, string searchItem, IEnumerable<string> fileList, ref List<SignalRResult> results)
        {
            foreach (string fileItem in fileList)
            {
                int counter = 1;
                string line;
                StreamReader file = new StreamReader(string.Format("{0}{1}{2}",
                    dirPath,
                    PathConstants.PATH_BACKSLASH,
                    Path.GetFileName(fileItem)));
                results.Add(new SignalRResult
                {
                    ItemValue = string.Format("{0}{1}{2}{3}",
                    MessagesConstants.FILE_NAME,
                    dirPath,
                    PathConstants.PATH_BACKSLASH,
                    Path.GetFileName(fileItem))
                });
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(searchItem))
                    {
                        results.Add(new SignalRResult
                        {
                            ItemValue = string.Format("{0}{1}{2}{3}",
                            CommonConstants.ROW,
                            counter.ToString(),
                            CommonConstants.COLON,
                            line.Substring(0, 100))
                        });
                    }

                    counter++;
                }

                file.Close();
            }
        }
    }
}