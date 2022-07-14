using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Broadcast_JWT.Models;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly SqliteDataContext _context;
        private IUserService _userService;
        public MessagesController(SqliteDataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {

            var msg = _context.Messages.Include(m => m.Votes).Include(m => m.Flags);
            return await msg.ToListAsync();
        }

        // GET: api/Messages ***DESCENDING ORDER***
        [HttpGet("descending")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesDesc()
        {
            var msg = _context.Messages.Include(m => m.Votes).Include(m => m.Flags);
            return await msg.OrderByDescending(d => d.DateStamp).ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, [Bind("MessageId,UserId,DateStamp,MessageTitle,MessageBody")] Message message)
        {

            if (id != message.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            JsonSerializerOptions options = new()
            {
                //switch to Always to ignore
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            var currentUser = (User)HttpContext.Items["User"];
            message.UserId = currentUser.Id;
            message.DateStamp = DateTime.Now;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~VOTING HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  
        // GET: api/Votes/5
        [HttpGet("Votes/{id}")]
        public async Task<ActionResult<Vote>> GetVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }

        // POST: api/Votes DOWNVOTE
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Downvote/{id}")]
        public async Task<ActionResult<Vote>> PostDownVote(int id, Vote vote)
        {
            vote.DownVote=true;
            var currentUser = (User)HttpContext.Items["User"];
            vote.UserId = currentUser.Id;
            // vote.MessageId = id;
            _context.Votes.Add(vote);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVote", new { id = vote.VoteId }, vote);
        }

        // POST: api/Votes UPVOTE
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Upvote/{id}")]
        public async Task<ActionResult<Vote>> PostUpVote(int id, Vote vote)
        {
            vote.UpVote=true;
            var currentUser = (User)HttpContext.Items["User"];
            vote.UserId = currentUser.Id;
            // vote.MessageId = id;
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVote", new { id = vote.VoteId }, vote);
        }

    }
}
