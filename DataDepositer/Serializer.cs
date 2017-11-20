/**
 * RuDiCon Soft (c) 2017
 * 
 * Class for Serialize / deserialize headers structure
 * 
 * @created 2017-08-29 Artem Niikolaev
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace DataDepositer
{
    public static class ObjectSerializer
    {
        // @return raw data of given object
        public static byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            byte[] rawdata = new byte[rawsize];
            GCHandle handle = GCHandle.Alloc(rawdata, GCHandleType.Pinned);
            Marshal.StructureToPtr(anything, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return rawdata;
        }

    }
}
