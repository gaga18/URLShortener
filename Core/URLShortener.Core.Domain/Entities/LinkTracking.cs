using Project.Core.Domain.Basics;
using System;

namespace URLShortener.Core.Domain.Entities
{
    public class LinkTracking : BaseEntity
    {
        public int LinkId { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public DateTime RequestTime { get; set; }

        public LinkEntity Link { get; set; }
    }
}
