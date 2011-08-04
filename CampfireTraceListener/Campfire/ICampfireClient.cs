using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampfireTraceListener.Campfire
{
    public interface ICampfireClient
    {
        void Join();
        void Post(string message);
        void Leave();
    }
}
