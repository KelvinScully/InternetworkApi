using ACommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Objects
{
    internal class Base
    {
        public readonly ConnectionOptions _ConnectionOptions;

        public Base(ConnectionOptions connectionOptions)
        {
            _ConnectionOptions = connectionOptions;
        }
    }
}
