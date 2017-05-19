using decodedon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace decodedon
{
    public class TootService
    {
        private static List<Federate> _federates;
        private static object _sync = new object();
        public TootService()
        {
            var setting = ConfigurationManager.AppSettings["Federates"] ?? "{}";

            if (_federates != null)
                return;

            lock (_sync)
            {
                if (_federates == null)
                {
                    _federates = new List<Federate>();
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(setting);
                    foreach (var kv in dict)
                    {
                        _federates.Add(new Federate { Name = kv.Key, Uri = new Uri(kv.Value) });
                    }
                }
            }
        }

        public IEnumerable<Toot> Load(bool hasFederated, int take = int.MaxValue)
        {
            var accessors = new List<ITootAccessor>
            {
                new TootLocal()
            };

            if (hasFederated)
                accessors.AddRange(_federates.Select(f => new TootRemote(f) as ITootAccessor));
#if NET35
            var sync = new object();
            var toots = new List<Toot>();
            var counter = accessors.Count;
            var wait = new System.Threading.AutoResetEvent(false);
            
            foreach (var accessor in accessors)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(acs =>
                {
                    var loaded = (acs as ITootAccessor).Load(take);

                    System.Threading.Interlocked.Decrement(ref counter);
                    lock (sync)
                    {
                        if (loaded != null)
                            toots.AddRange(loaded);
                    }

                    if (counter <= 0)
                        wait.Set();
                }, accessor);
            }
            wait.WaitOne();
#else
            var toots = new System.Collections.Concurrent.ConcurrentBag<Toot>();
            var tasks = accessors.Select(acs => System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                var loaded = (acs as ITootAccessor).Load(take);
                if (loaded != null)
                    foreach (var t in loaded)
                        toots.Add(t);
            }));
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
#endif
            return toots.OrderByDescending(t => t.CreateAt).ThenBy(t => t.IsRemote);
        }

        public void Add(Toot toot)
        {
            var local = new TootLocal();
            local.Add(toot);
        }
    }
}
