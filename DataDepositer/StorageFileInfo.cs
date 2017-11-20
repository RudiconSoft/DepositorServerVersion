﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    [Serializable]
    public class StorageFileInfo
    {
        /// <summary>
        /// Gets or sets the virtual path to the file
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// Gets or sets the size of the file (in bytes)
        /// </summary>
        public long Size { get; set; }

    }
}
