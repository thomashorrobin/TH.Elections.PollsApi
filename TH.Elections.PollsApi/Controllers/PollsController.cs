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
        public IEnumerable<Polls> GetPolls()
        {
            var x = _context.Polls.ToList().Select(p => { p.PartyVotingIntentions = null; return p; });
            return x;
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