using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampfireTraceListener.Configuration
{
    public interface ICampfireConfiguration
    {
        string AccountName { get; set; }
        string AuthToken { get; set; }
        int RoomId { get; set; }
        bool UseHttps { get; set; }
    }
}
