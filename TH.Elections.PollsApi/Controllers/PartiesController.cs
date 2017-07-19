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
    [Route("api/Parties")]
    public class PartiesController : Controller
    {
        private readonly GeneralElection2014Context _context;

        public PartiesController(GeneralElection2014Context context)
        {
            _context = context;
        }

        // GET: api/Parties
        [HttpGet]
        public IEnumerable<PoliticalParties> GetPoliticalParties(bool all = true)
        {
            if (all)
            {
                return _context.PoliticalParties.ToList().Select(pp => { pp.PartyVotingIntentions = null; return pp; });
            }
            else
            {
                return _context.PoliticalParties.Where(pp => pp.CurrentlyPolling).ToList().Select(pp => { pp.PartyVotingIntentions = null; return pp; });
            }
        }

        // GET: api/Parties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoliticalParties([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var politicalParties = await _context.PoliticalParties.SingleOrDefaultAsync(m => m.PartyId == id);

            if (politicalParties == null)
            {
                return NotFound();
            }

            politicalParties.PartyVotingIntentions = _context.PartyVotingIntentions.Where(pvi => pvi.PartyId == id).ToList();

            return Ok(politicalParties);
        }

        private bool PoliticalPartiesExists(int id)
        {
            return _context.PoliticalParties.Any(e => e.PartyId == id);
        }
    }
}