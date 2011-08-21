using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CampfireTraceListener.Campfire;

namespace CampfireTraceListeners
{
    public class CampfireTraceListener : CustomTraceListener 
    {
        private readonly ICampfireClient _client;

        public CampfireTraceListener()
            :this(new CampfireClient())
        {
            
        }

        public CampfireTraceListener(ICampfireClient client)
            : base("CampfireTraceListener")
        {
            _client = client;
        }

        #region CustomTraceListener Overrides

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all other TraceEvent methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="message">A message to be output regarding the trace event</param>
        protected override void TraceEventCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                               int id, string message)
        {
            SendCampfireMessage(eventCache, source, eventType, id, message, null);
        }

        /// <summary>
        /// This method must be overriden and forms the core logging method called by all otherTraceData methods.
        /// </summary>
        /// <param name="eventCache">A cache of data that defines the trace event</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The unique ID of the trace event</param>
        /// <param name="data">The data to be logged</param>
        protected override void TraceDataCore(TraceEventCache eventCache, string source, TraceEventType eventType,
                                              int id, params object[] data)
        {
            SendCampfireMessage(eventCache, source, eventType, id, null, data);
        }

        #endregion

        private void SendCampfireMessage(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            // iterate through the data and create a single message
            StringBuilder sb = new StringBuilder();

            foreach (object item in data.Where(item => item != null))
                sb.Append(item);

            // Source LogLevel (id): message
            string message = string.Format("{0} {1} ({2}): {3}", source, eventType.ToString(), id, sb.ToString());

            _client.Post(message);
        }
    }
}