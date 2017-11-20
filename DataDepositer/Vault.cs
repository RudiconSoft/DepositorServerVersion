/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for Static Data Vault.
 * 
 * @created 2017-10-10 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public static class Vault
    {
        public static List<StorageItem> StorageList = new List<StorageItem>();
        public static List<SendItem> SendList = new List<SendItem>();
        public static List<StorageItem> AssembleList = new List<StorageItem>();
        public static CommandQueue MainQueue = new CommandQueue();
        public static List<PeerEntry> Peers = new List<PeerEntry>();
        public static LocalInfoData LocalInfo = new LocalInfoData();
    }
}
