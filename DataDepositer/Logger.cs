using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;


namespace DataDepositer
{
    public static class Logger
    {
       public static NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
    }
}
