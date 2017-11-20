/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for Queue of Command manipulations.
 * 
 * @created 2017-09-27 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public class CommandQueue : Queue<Command>
    {

        public CommandQueue()
        {
            //string uniqueKey = Guid.NewGuid().ToString();

        }
    }
}
