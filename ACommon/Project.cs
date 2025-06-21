using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACommon
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool IsLocal { get; set; }
        public bool IsIISExpress { get; set; }
    }
}
