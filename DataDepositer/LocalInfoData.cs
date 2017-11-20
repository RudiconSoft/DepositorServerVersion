/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for Local Data information.
 * 
 * @created 2017-10-10 Artem Niikolaev
 * 
 */
using System;

namespace DataDepositer
{
    [Serializable]
    public class LocalInfoData
    {
        public string UserName { get; set; }
        public string MachineName { get; set; }
        public string Comment { get; set; }
        //public object Data = null; // 

        public LocalInfoData()
        {
            
        }
        public LocalInfoData(string username, string machinename, string comment)
        {
            UserName = username;
            machinename = MachineName;
            Comment = comment;
        }
    }
}