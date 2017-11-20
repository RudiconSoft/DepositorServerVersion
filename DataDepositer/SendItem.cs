/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for Store Info (Storage, Send, Assemble)
 * 
 * @created 2017-09-27 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    [Serializable]
    public class SendItem
    {
        public string Name { get; set; }
        public string OriginName { get; set; }
        public string Description { get; set; }
        public UInt64 Size { get; set; }
        public uint Chunks { get; set; }
        public uint ChunkNum { get; set; }
        public string MD5Chunk { get; set; }
        public string MD5Origin { get; set; }
        public uint SendedChunks { get; set; }



        public SendItem()
        {

        }

        public SendItem(string name, string originname, string desc,
                            UInt64 size, uint chunks, uint chunknum, string md5Chunk, string md5origin, uint sendedChunks)
        {
            Name = name;
            OriginName = originname;
            Description = desc;
            Size = size;
            Chunks = chunks;
            ChunkNum = chunknum;
            MD5Chunk = md5Chunk;
            MD5Origin = md5origin;
            SendedChunks = sendedChunks;
        }

        public string[] ToViewStrings
        {
            get
            {
                string[] str = new string[6];
                //string[] str = new string[]{ };

                str[0] = Name;
                str[1] = OriginName;
                str[2] = Description;
                str[3] = Convert.ToString(Size);
                str[4] = Convert.ToString(ChunkNum);
                str[5] = Convert.ToString(Chunks);

                return str;
            }
        }
    }
}
