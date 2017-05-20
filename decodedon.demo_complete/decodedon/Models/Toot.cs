using System;

namespace decodedon.Models
{
    public struct Toot
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsRemote { get; set; }

        public Toot ToRemote()
        {
            IsRemote = true;
            return this;
        }

        public Toot ToFederate(string federateName)
        {
            // c# 6 features : String interpolation
            Name = $"{Name}@{federateName}";
            return this;
        }
    }
}
