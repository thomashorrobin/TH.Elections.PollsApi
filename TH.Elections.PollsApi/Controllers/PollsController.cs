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
    [Route("api/Polls")]
    public class PollsController : Controller
    {
        private readonly GeneralElection2014Context _context;

        public PollsController(GeneralElection2014Context context)
        {
            _context = context;
        }

        // GET: api/Polls
        [HttpGet]
        public IEnumerable<Polls> GetPolls(int? limit = null)
        {
            if (limit == null)
            {
                return _context.Polls.ToList().Select(p => { p.PartyVotingIntentions = null; return p; });
            }
            else
            {
                List<Polls> polls = _context.Polls.OrderByDescending(p => p.PollDate).Take((int)limit).Include(p => p.PartyVotingIntentions).Include(p => p.PollingCompany).ToList();
                List<int> pollingCompanyIDs = polls.Select(p => p.PollingCompanyId).ToList();
                List<PollingCompanies> pollingCompanies = _context.PollingCompanies.Where(pc => pollingCompanyIDs.Any(pcid => pcid == pc.PollingCompanyId)).ToList();
                foreach (Polls poll in polls)
                {
                    PollingCompanies pollingCompany = pollingCompanies.Single(pc => pc.PollingCompanyId == poll.PollingCompanyId);
                    pollingCompany.Polls = null;
                    poll.PollingCompany = pollingCompany;
                }
                return polls;
            }
        }

        // GET: api/Polls/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolls([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var polls = await _context.Polls.SingleOrDefaultAsync(m => m.PollId == id);

            if (polls == null)
            {
                return NotFound();
            }

            polls.PartyVotingIntentions = _context.PartyVotingIntentions.Where(pvi => pvi.PollId == id).ToList();

            return Ok(polls);
        }

        private bool PollsExists(int id)
        {
            return _context.Polls.Any(e => e.PollId == id);
        }
    }
}