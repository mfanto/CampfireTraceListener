using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CampfireTraceListener.Campfire;

namespace CampfireTraceListener.Demo
{
    class Program
    {
        private static readonly TraceSource _ts = new TraceSource("CampfireTraceListenerDemo");

        static void Main(string[] args)
        {
            _ts.TraceEvent(TraceEventType.Error, 0, "Traced through campfire");
        }
    }
}
