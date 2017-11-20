using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public class FileManager
    {
        public TcpClient tcp = null;
        public TcpListener clientListener = null;

        private string workdir = string.Empty;

        public bool Recieved { get; set; }

        public FileManager(string workdir)
        {
            this.workdir = workdir;
            tcp = new TcpClient();
        }

        public FileManager()
        {
            //            this.workdir = workdir;
            tcp = new TcpClient();
        }


        public bool Send(string filename, IPEndPoint endpoint)
        {
            bool res = true;

            try
            {
                //TcpClient eclient = new TcpClient("localhost", 34567);
                //TcpClient eclient = new TcpClient(endpoint);
                tcp = new TcpClient(endpoint);
                NetworkStream writerStream = tcp.GetStream();
                BinaryFormatter format = new BinaryFormatter();
                byte[] buf = new byte[1024];
                int count;
                FileStream fs = new FileStream(filename, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                long k = fs.Length;//Размер файла.
                format.Serialize(writerStream, k.ToString());//Вначале передаём размер
                while ((count = br.Read(buf, 0, 1024)) > 0)
                {
                    format.Serialize(writerStream, buf);//А теперь в цикле по 1024 байта передаём файл
                }
            }
            catch (System.Exception)
            {
                res = false;
                //throw;
            }

            return res;
        }

        public bool Recieve(string filename, IPEndPoint endpoint)
        {
            bool res = true;

            try
            {
                //TcpListener clientListener = new TcpListener("localhost", 34567);
                clientListener = new TcpListener(endpoint);
                clientListener.Start();
                TcpClient client = clientListener.AcceptTcpClient();
                NetworkStream readerStream = client.GetStream();
                BinaryFormatter outformat = new BinaryFormatter();
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryWriter bw = new BinaryWriter(fs);
                int count;
                count = int.Parse(outformat.Deserialize(readerStream).ToString());//Получаем размер файла
                int i = 0;
                for (; i < count; i += 1024) //Цикл пока не дойдём до конца файла
                {
                    byte[] buf = (byte[])(outformat.Deserialize(readerStream));//Собственно читаем из потока и записываем в файл
                    bw.Write(buf);
                }

                bw.Close();
                fs.Close();
            }
            catch (System.Exception)
            {
                res = false;
                //throw;
            }

            return res;
        }
    }
}
