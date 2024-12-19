using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.Tools
{
    public static class MemClear
    {
        public static void Clear() 
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
