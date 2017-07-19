using System;
using System.Collections.Generic;

namespace TH.Elections.PollsApi.Models
{
    public partial class PollingCompanies
    {
        public PollingCompanies()
        {
            Polls = new HashSet<Polls>();
        }

        public int PollingCompanyId { get; set; }
        public string PollingCompanyName { get; set; }
        public bool CurrentlyPolling { get; set; }

        public virtual ICollection<Polls> Polls { get; set; }
    }
}
