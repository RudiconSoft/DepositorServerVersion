// Файл P2PService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO;

namespace DataDepositer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class P2PService : IP2PService
    {
        private MainForm hostReference;
        private string username;

        public P2PService(object hostReference, string username)
        {
            this.hostReference = (MainForm) hostReference;
            this.username = username;
        }

        public string GetName()
        {
            return username;
        }

        public void SendMessage(string message, string from)
        {
            hostReference.DisplayMessage(message, from);
            // Async Command processing
        }

        public void SendCommand(Command command, string from)
        {
            //hostReference.DisplayMessage(message, from);
            // Async Command processing
            hostReference.ReciveCommand(command, from);
            //Vault.MainQueue.Enqueue(command);
           
            //Logger.Log.Info(string.Format("Recieve command {0} from : {1}", command.Message, from));
        }

        public RemoteFileInfo DownloadFile(DownloadRequest request)
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

        public RemoteFileInfo EchoFile(DownloadRequest request)
        {
            //throw (new NotImplementedException());
            return new RemoteFileInfo();
        }

        public RemoteFileInfo UploadFile(UploadRequest request)
        {
            //throw (new NotImplementedException());
            return new RemoteFileInfo();
        }
        public RemoteListInfo RequestList()
        {
            //throw (new NotImplementedException());
            //return new RemoteListInfo();
            return hostReference.RequestList();
        }


    }
}