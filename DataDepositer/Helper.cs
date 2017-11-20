/**
 *  RuDiCon Soft (c) 2017 
 * 
 *  Helper class for using by Data Depositor
 * 
 *  @created 2017-08-28 Artem Nikolaev
 *  @updated 2017-08-28 Artem Nikolaev 
 *                      Added MD5 maipulation helper.
 *  @updated 2017-09-01 Artem Nikolaev
 *                      Replace in STORED_FILE_HEADER char[] to string
 *  @updated 2017-10-10 Artem Nikolaev
 *                      Added methods: SerializeToXML, DeserializeFromXML. 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace DataDepositer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct STORED_FILE_HEADER // (668 in Unicode)
    {
        public uint cb;             // Structure size // 4 // indicator for non-initialized struct

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string FileName;     // Short file name

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Description;     // Short file name

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string MD5Origin;    // String MD5 of origin file //@TODO (Base64 or text ADB347..) 

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string MD5Chunk;     // String MD5 of chunk

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string MD5Userid;     // String MD5 of username + password for get list of users stored files

        public UInt64 ChunksQty;      // Chunks qty // 8

        public UInt64 ChunkNum;       // Chunks number // 8

        public UInt64 OriginSize;     // origin file size //8

        public UInt64 ChunkSize;      // chunk size   //8
    }


    public class Helper
    {
        // @created 2017-08-28 Artem Nikolaev
        // MD5 manipulation helper.
        public String GetStringMD5(String key)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //byte[] inputBytes = Encoding.ASCII.GetBytes(key);
            byte[] inputBytes = Encoding.Unicode.GetBytes(key);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            //// Convert the byte array to hexadecimal string
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < hashBytes.Length; i++)
            //{
            //    sb.Append(hashBytes[i].ToString("X2"));
            //}
            //return sb.ToString();
            return Convert.ToBase64String(hashBytes);
        }

        public String GetStringMD5(byte[] inputBytes)
        {
            byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(inputBytes);

            return Convert.ToBase64String(hashBytes);
        }

        public string GetFileMD5(String filename)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            FileStream fs = File.OpenRead(filename);
            byte[] hashBytes = md5.ComputeHash(fs);

            //// Convert the byte array to hexadecimal string
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < hashBytes.Length; i++)
            //{
            //    sb.Append(hashBytes[i].ToString("X2"));
            //}
            //return sb.ToString();
            return Convert.ToBase64String(hashBytes);
        }


        //
        // Read struct bytes from filesteram and return struct
        // 
        // @use SOMESTRUCTURE stExmpl = ReadStruct<SOMESTRUCTURE>(filesteram);
        //
        //
        public T ReadStruct<T>(FileStream fs)
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            fs.Read(buffer, 0, Marshal.SizeOf(typeof(T)));
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            T temp = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return temp;
        }

        public object ReadStruct(FileStream fs, Type t)
        {
            byte[] buffer = new byte[Marshal.SizeOf(t)];
            fs.Read(buffer, 0, Marshal.SizeOf(t));
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Object temp = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), t);
            handle.Free();
            return temp;
        }

        //
        // @return raw data of given object
        //
        public byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            byte[] rawdata = new byte[rawsize];
            GCHandle handle = GCHandle.Alloc(rawdata, GCHandleType.Pinned);
            Marshal.StructureToPtr(anything, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return rawdata;
        }

        //public byte[] RawSerialize(object anything)
        //{
        //    int rawsize = Marshal.SizeOf(anything);
        //    IntPtr buffer = Marshal.AllocHGlobal(rawsize);
        //    Marshal.StructureToPtr(anything, buffer, false);
        //    byte[] rawdata = new byte[rawsize];
        //    Marshal.Copy(buffer, rawdata, 0, rawsize);
        //    Marshal.FreeHGlobal(buffer);
        //    return rawdata;
        //}


        // fill STORED_FILE_HEADER
        public STORED_FILE_HEADER FillHeader(   
                                                string FileName,     // Short file name
                                                string Desc,         // Description
                                                string MD5Origin,    // String MD5 of origin file
                                                string MD5Chunk,     // String MD5 of chunk
                                                uint ChunksQty,      // Chunks qty 
                                                uint ChunkNum,       // Chunks number 
                                                uint OriginSize,     // origin file size 
                                                uint ChunkSize       // chunk size   
                                            )
        {
            STORED_FILE_HEADER sfh = new STORED_FILE_HEADER();

            sfh.FileName = FileName;
            sfh.Description = Desc;
            sfh.MD5Origin = MD5Origin;
            sfh.MD5Chunk = MD5Chunk;
            sfh.ChunksQty = ChunksQty;
            sfh.ChunkNum = ChunkNum;
            sfh.OriginSize = OriginSize;
            sfh.ChunkSize = ChunkSize;

            sfh.cb = (uint) Marshal.SizeOf(sfh); // size of struct

            return sfh;
        }

        // @return string with current application data folder

        public string GetCurrentAppDataFolder()
        {
            string res = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return res;
        }

        public void SerializeToXML<T>(string filename, T obj)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, obj);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T DeserializeFromXML<T>(string filename)
        {
            T obj = default(T);
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                  obj = (T)formatter.Deserialize(fs);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return obj;
        }

        #region Static Region
        public static NetTcpBinding GetStreamBinding()
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None; // need 

            /////
            /////
            // Stream add
            ////
            ////
            binding.TransferMode = TransferMode.Streamed;
            binding.MaxReceivedMessageSize = 20134217728; // 20 GB
            binding.MaxBufferPoolSize = 1024 * 1024; // 1 MB
            binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
            binding.SendTimeout = new TimeSpan(1, 0, 0);
            ////
            ////
            ////
            ////

            return binding;
        }


        #endregion
    }
}
