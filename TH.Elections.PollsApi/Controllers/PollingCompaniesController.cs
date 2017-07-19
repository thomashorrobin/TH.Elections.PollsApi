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
    [Route("api/PollingCompanies")]
    public class PollingCompaniesController : Controller
    {
        private readonly GeneralElection2014Context _context;

        public PollingCompaniesController(GeneralElection2014Context context)
        {
            _context = context;
        }

        // GET: api/PollingCompanies
        [HttpGet]
        public IEnumerable<PollingCompanies> GetPollingCompanies()
        {
            return _context.PollingCompanies.Where(pc => pc.CurrentlyPolling).ToList().Select(pc => { pc.Polls = null; return pc; });
        }

        // GET: api/PollingCompanies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPollingCompanies([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pollingCompanies = await _context.PollingCompanies.SingleOrDefaultAsync(m => m.PollingCompanyId == id);

            if (pollingCompanies == null)
            {
                return NotFound();
            }

            pollingCompanies.Polls = _context.Polls.Where(p => p.PollingCompanyId == id).ToList().Select(p => { p.PollingCompany = null; p.PartyVotingIntentions = null; return p; }).ToList();

            return Ok(pollingCompanies);
        }

        private bool PollingCompaniesExists(int id)
        {
            return _context.PollingCompanies.Any(e => e.PollingCompanyId == id);
        }
    }
}