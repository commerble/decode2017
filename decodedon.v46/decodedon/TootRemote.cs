using decodedon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace decodedon
{
    public class TootRemote : ITootAccessor
    {
        private Federate _federate;

        // c# 7 features : expression bodies - constructor
        public TootRemote(Federate federate) => _federate = federate;

        public IEnumerable<Toot> Load(int take, int skipToken = 0)
        {
            using (var wc = new WebClient { Encoding = Encoding.UTF8 })
            {
                var payload = wc.DownloadString(_federate.Uri);
                var json = JsonConvert.DeserializeObject<RemoteToots>(payload);

                return json.Data.Select(t => t.ToFederate(_federate.Name));
            }
        }

        public bool Add(Toot toot)
        {
            throw new NotImplementedException();
        }

        class RemoteToots
        {
            [JsonProperty("d")]
            public IEnumerable<Toot> Data { get; set; }
        }
    }
}
