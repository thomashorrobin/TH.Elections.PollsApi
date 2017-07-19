using System;
using System.Collections.Generic;

namespace TH.Elections.PollsApi.Models
{
    public partial class PoliticalParties
    {
        public PoliticalParties()
        {
            PartyVotingIntentions = new HashSet<PartyVotingIntentions>();
        }

        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string ShortName { get; set; }
        public string PartyColour { get; set; }
        public bool CurrentlyPolling { get; set; }

        public virtual ICollection<PartyVotingIntentions> PartyVotingIntentions { get; set; }
    }
}
