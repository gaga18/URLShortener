using Project.Core.Domain.Basics;
using System.Collections.Generic;

namespace URLShortener.Core.Domain.Entities
{
    public class LinkEntity : AuditableEntity
    {
        public string OriginUrl { get; set; }
        public string FwToken { get; set; }
        public string Note { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<LinkTracking> LinkTrackings { get; set; }
    }
}
