using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH.Elections.PollsApi.Models;

namespace TH.Elections.PollsApi.ViewModels
{
    public class GraphData
    {
        public IEnumerable<Polls> Polls { get; set; }
        public IEnumerable<PoliticalParties> Parties { get; set; }
        public IEnumerable<PollingCompanies> PollingCompanies { get; set; }
        public IEnumerable<PartyVotingIntentions> PVIs { get; set; }
    }
}
