using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CampfireTraceListener.Campfire;

namespace CampfireTraceListener
{
    public class CampfireTraceListener : TraceListener 
    {
        private readonly ICampfireClient _client;

        public CampfireTraceListener()
            :this(new CampfireClient())
        {
            
        }

        public CampfireTraceListener(ICampfireClient client)
        {
            _client = client;
        }

        public void Join()
        {
            _client.Join();
        }

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write. </param><filterpriority>2</filterpriority>
        public override void Write(string message)
        {
            _client.Post(message);
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write. </param><filterpriority>2</filterpriority>
        public override void WriteLine(string message)
        {
            _client.Post(message);
        }
    }
}