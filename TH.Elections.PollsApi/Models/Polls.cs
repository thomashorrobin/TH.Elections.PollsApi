using System;
using System.Collections.Generic;

namespace TH.Elections.PollsApi.Models
{
    public partial class Polls
    {
        public Polls()
        {
            PartyVotingIntentions = new HashSet<PartyVotingIntentions>();
        }

        public int PollId { get; set; }
        public int PollingCompanyId { get; set; }
        public DateTime PollDate { get; set; }

        public virtual ICollection<PartyVotingIntentions> PartyVotingIntentions { get; set; }
        public virtual PollingCompanies PollingCompany { get; set; }
    }
}
