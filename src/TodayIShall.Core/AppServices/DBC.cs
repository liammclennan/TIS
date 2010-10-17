using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodayIShall.Core.AppServices
{
    public class DBC
    {
        public static void Assert(bool condition, string message)
        {
            if (!condition) throw new DBCException(message);
        }
    }

    public class DBCException : Exception
    {
        public DBCException(string message) : base(message)
        {}
    }
}
