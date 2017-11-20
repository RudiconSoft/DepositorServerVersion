/**
 * RuDiCon Soft (c) 2017
 * 
 * P2P service interface.
 * 
 * @created 2017-10-10 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.IO;

namespace DataDepositer
{
    [ServiceContract]
    public interface IP2PService
    {
        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
        RemoteFileInfo EchoFile(DownloadRequest request);

        [OperationContract]
        RemoteFileInfo UploadFile(UploadRequest request);

        [OperationContract]
        RemoteListInfo RequestList();


        [OperationContract]
        string GetName();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string from);

        [OperationContract(IsOneWay = true)]
        void SendCommand(Command command, string from);
    }

    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class UploadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    [MessageContract]
    public class RemoteStorageInfo
    {
        [MessageHeader(MustUnderstand = true)]
        public List<STORED_FILE_HEADER> FileList;
    }

    [MessageContract]
    public class RemoteListInfo
    {
        [MessageHeader(MustUnderstand = true)]
        public List<StorageFileInfo> FileList = new List<StorageFileInfo>();
    }

}