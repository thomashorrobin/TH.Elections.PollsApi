using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TH.Elections.PollsApi.Models
{
    public partial class PartyVotingIntentions
    {
        public int PartyId { get; set; }
        public int PollId { get; set; }
        public double PartyVote { get; set; }

        [JsonIgnore]
        public virtual PoliticalParties Party { get; set; }
        [JsonIgnore]
        public virtual Polls Poll { get; set; }
    }
}
