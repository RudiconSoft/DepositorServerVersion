/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for P2P Network Entry data.
 * 
 * @created 2017-09-28 Artem Niikolaev
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.PeerToPeer;
using System.Net;

namespace DataDepositer
{
    public class PeerEntry
    {
        public PeerName PeerName { get; set; }
        public IP2PService ServiceProxy { get; set; }
        public string DisplayString { get; set; }
        public string Comment { get; set; }
        //public bool ButtonsEnabled { get; set; }
        public string Data { get; set; }
        //public IPEndPointCollection Endpoints;
        public override string ToString()
        {
            return Comment;
        }
    }
}