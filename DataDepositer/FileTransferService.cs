/**
 * RuDiCon Soft (c) 2017
 * 
 * File transfer service interface implimentation.
 * 
 * @created 2017-10-10 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer_2
{
    public class FileTransferService : IFileTransferService
    {
        public RemoteFileInfo DownloadFile(DownloadRequest1 request)
        {
            var filePath = request.FileName;
            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists == false)
            {
                throw new FileNotFoundException("File not found", request.FileName);
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var result = new RemoteFileInfo
            {
                FileName = request.FileName,
                Length = fileInfo.Length,
                FileByteStream = stream
            };

            return result;
        }
    }
}
