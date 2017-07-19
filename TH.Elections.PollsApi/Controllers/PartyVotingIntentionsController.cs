using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH.Elections.PollsApi.Models;

namespace TH.Elections.PollsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/PartyVotingIntentions")]
    public class PartyVotingIntentionsController : Controller
    {
        private readonly GeneralElection2014Context _context;

        public PartyVotingIntentionsController(GeneralElection2014Context context)
        {
            _context = context;
        }

        // GET: api/PartyVotingIntentions
        [HttpGet]
        public IEnumerable<PartyVotingIntentions> GetPartyVotingIntentions()
        {
            return _context.PartyVotingIntentions;
        }
    }
}