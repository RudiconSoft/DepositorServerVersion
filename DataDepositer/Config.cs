/**
 *  RuDiCon Soft (c) 2017
 * 
 *  Class for Store & manipulate application parameters
 *  
 *  @created 2017-09-19 Artem Nikolaev 
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public class Config
    {
        public string AppDataFolder;

        public string StorageFolder;
        public string SendFolder;
        public string AssembleFolder;

        public string TempFolder;

        public string DDNSAddress;
        public string DDNSUserName;
        public string DDNSPassword;

        public uint Port; // 13027
        public uint StorageSize;// = 1024 * 1024 * 1024; // 1Gb

        public uint Chunks; // 3

        public Config()
        {
            string appdata = new Helper().GetCurrentAppDataFolder();

            AppDataFolder   = appdata       + "\\DataDepositor";
            StorageFolder   = AppDataFolder + "\\Storage\\";
            SendFolder      = AppDataFolder + "\\Send\\";
            AssembleFolder  = AppDataFolder + "\\Assemble\\";
            TempFolder      = AppDataFolder + "\\Temp\\"; // @TODO add clean after exit.

            StorageSize = 3 * 1024 * 1024; // 1Gb
            Port = 13027;

            Chunks = 3;
        }

        public void FromINI(IniFile ini)
        {
            // Folders
            AppDataFolder = ini.Read(GetFolderSectionText(), GetAppDataFolderText());
            StorageFolder = ini.Read(GetFolderSectionText(), GetStorageFolderText());
            SendFolder = ini.Read(GetFolderSectionText(), GetSendFolderText());
            AssembleFolder = ini.Read(GetFolderSectionText(), GetAssembleFolderText());
            TempFolder = ini.Read(GetFolderSectionText(), GetTempFolderText());

            // Network
            DDNSAddress = ini.Read(GetNetworkSectionText(), GetDDNSAddressText());
            DDNSUserName = ini.Read(GetNetworkSectionText(), GetDDNSUserNameText());
            DDNSPassword = ini.Read(GetNetworkSectionText(), GetDDNSPasswordText());
            Port = Convert.ToUInt16(ini.Read(GetNetworkSectionText(), GetPortText()));

            // Parameters
            Chunks = Convert.ToUInt16(ini.Read(GetParamSectionText(), GetChunksText()));
        }

        public void ToINI(IniFile ini)
        {
            // Folders
             ini.Write(GetFolderSectionText(), GetAppDataFolderText(), AppDataFolder);
             ini.Write(GetFolderSectionText(), GetStorageFolderText(), StorageFolder);
             ini.Write(GetFolderSectionText(), GetSendFolderText(), SendFolder);
             ini.Write(GetFolderSectionText(), GetAssembleFolderText(), AssembleFolder);
             ini.Write(GetFolderSectionText(), GetTempFolderText(), TempFolder);

            // Network
             ini.Write(GetNetworkSectionText(), GetDDNSAddressText(), DDNSAddress);
             ini.Write(GetNetworkSectionText(), GetDDNSUserNameText(), DDNSUserName);
             ini.Write(GetNetworkSectionText(), GetDDNSPasswordText(), DDNSPassword);
             ini.Write(GetNetworkSectionText(), GetPortText(), Convert.ToString(Port));

            // Paramerets
             ini.Write(GetParamSectionText(), GetChunksText(), Convert.ToString(Chunks));
        }


        /// <summary>
        ///  Section for text names for Folders Section INI file
        /// </summary>
        public string GetAppDataFolderText()
        {
            return "AppDataFolder";
        }
        public string GetStorageFolderText()
        {
            return "StorageFolder";
        }

        public string GetSendFolderText()
        {
            return "SendFolder";
        }

        public string GetAssembleFolderText()
        {
            return "AssembleFolder";
        }

        public string GetTempFolderText()
        {
            return "TempFolder";
        }

        /// <summary>
        /// Section for text names for Network Section INI file
        /// </summary>
        public string GetDDNSAddressText()
        {
            return "DDNSAddress";
        }

        public string GetDDNSUserNameText()
        {
            return "DDNSUserName";
        }

        public string GetDDNSPasswordText()
        {
            return "DDNSPassword";
        }

        public string GetPortText()
        {
            return "Port";
        }

        /// <summary>
        ///  Section for text names for Parameters Section INI file
        /// </summary>
        public string GetChunksText()
        {
            return "Chunks";
        }



        /// <summary>
        /// Section names
        /// </summary>
        public string GetFolderSectionText()
        {
            return "Folders";
        }

        public string GetParamSectionText()
        {
            return "Parameters";
        }

        public string GetNetworkSectionText()
        {
            return "Network";
        }

        public string[] GetLocalStorageViewColumns()
        {
            string[] columns = { "File Name", "Description", "Size", "Chunks", "Status" };
            return columns;
        }

        public string[] GetLocalSendViewColumns()
        {
            string[] columns = { "File Name", "Description", "Size", "Chunks", "Status", "Sended / Remain" };
            return columns;
        }

    }
}
