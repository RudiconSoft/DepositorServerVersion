/**
 * RuDiCon Soft (c) 2017
 * 
 * File transfer service interface
 * 
 * @created 2017-10-10 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer_2
{
    [ServiceContract]
    public interface IFileTransferService
    {
       // [OperationContract]
        //RemoteFileInfo DownloadFile(DownloadRequest request);
    }

    [MessageContract]
    public class DownloadRequest1
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
}
