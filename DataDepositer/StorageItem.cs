/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for Store Info (Storage, 
 * 
 * @created 2017-08-29 Artem Niikolaev
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
    public class StorageItem
    {
        public string Name { get; set; }
        public string OriginName { get; set; }
        public string Description { get; set; }
        public UInt64 Size { get; set; }
        public uint Chunks { get; set; }
        public uint ChunkNum { get; set; }
        public string MD5Chunk { get; set; }
        public string MD5Origin { get; set; }


        public StorageItem()
        {

        }

        public StorageItem(string name, string originname, string desc, 
                            UInt64 size, uint chunks, uint chunknum, string md5Chunk, string md5origin)
        {
            Name = name;
            OriginName = originname;
            Description = desc;
            Size = size;
            Chunks = chunks;
            ChunkNum = chunknum;
            MD5Chunk = md5Chunk;
            MD5Origin = md5origin;
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
