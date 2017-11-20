/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for help build Command.
 * 
 * @created 2017-09-28 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public enum CommandType
    {
        None,
        TestConnect,
        Ready,
        StoreFile,
        SendFile,
        RequestFile,
        RequestChunk,
        CheckFile,
        CheckChunk
    }

    public class CommandBuilder
    {
        //public CommandType type;

        public CommandBuilder()
        {

        }
    }
}
