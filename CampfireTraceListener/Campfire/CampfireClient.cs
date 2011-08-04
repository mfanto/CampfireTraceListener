using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using CampfireTraceListener.Configuration;

namespace CampfireTraceListener.Campfire
{
    /// <summary>
    /// Campfire Client for interacting with the service
    /// </summary>
    /// <remarks>
    /// The code for interacting with Campfire comes from the ccnet.campfire.plugin code by ALex Cordellis. 
    /// </remarks>
    public class CampfireClient : ICampfireClient
    {
        private readonly string _accountName;
        private readonly string _authToken;
        private readonly int _roomId;
        private readonly bool _useHttps;

        public CampfireClient()
            :this(CampfireConfiguration.Instance)
        {
            
        }

        public CampfireClient(ICampfireConfiguration configuration)
        {
            _accountName = configuration.AccountName;
            _authToken = configuration.AuthToken;
            _roomId = configuration.RoomId;
            _useHttps = configuration.UseHttps;
        }

        public void Join()
        {
            PostTo("join");
        }

        public void Post(string message)
        {
            PostTo("speak",
                   req =>
                   {
                       using (var sw = new XmlTextWriter(req.GetRequestStream(), Encoding.UTF8))
                       {
                           sw.WriteStartDocument();
                           sw.WriteStartElement("message");
                           sw.WriteElementString("type", "TextMessage");
                           sw.WriteElementString("body", message);
                           sw.WriteEndElement();
                           sw.WriteEndDocument();
                       }
                   });
        }

        //public IEnumerable<string> Transcript
        //{
        //    get
        //    {
        //        var webResponse = GetFrom("transcript");
        //        using (var stream = webResponse.GetResponseStream())
        //        using (var reader = new StreamReader(stream))
        //            return new CampfireTranscriptReader().Process(reader.ReadToEnd());
        //    }
        //}

        public void Leave()
        {
            PostTo("leave");
        }

        private void PostTo(string action)
        {
            PostTo(action, req => { });
        }

        private WebResponse GetFrom(string action)
        {
            var request = RequestTo(action, "GET");
            return request.GetResponse();
        }


        private void PostTo(string action, Action<HttpWebRequest> requestInfo)
        {
            var request = RequestTo(action, "POST");
            request.ContentType = "application/xml";
            requestInfo(request);
            request.GetResponse();
        }

        private HttpWebRequest RequestTo(string action, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(string.Format("http{3}://{0}.campfirenow.com/room/{1}/{2}.xml", _accountName, _roomId, action, _useHttps ? "s" : ""));
            request.Method = method;
            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", EncodedAuthToken);
            request.Accept = "application/xml";
            return request;
        }

        private string EncodedAuthToken
        {
            get
            {
                var bytesToEncode = Encoding.ASCII.GetBytes(_authToken + ":X");
                return Convert.ToBase64String(bytesToEncode);
            }
        }

    }
}
