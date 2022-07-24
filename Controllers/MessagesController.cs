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
using WebApi.Authorization;
using System.Data;

namespace WebApi.Controllers
{
    [Authorize]
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

        // GET: api/Messages ***ASCENDING ORDER***
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {

            var msg = _context.Message.Include(m => m.Votes)
                                        .Include(m => m.Flags)
                                        .Include(m => m.Responses)
                                        .Include(m => m.AppUser);
            return await msg.ToListAsync();
        }

        // GET: api/Messages ***DESCENDING ORDER***
        [HttpGet("descending")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesDesc()
        {
            var msg = _context.Message.Include(m => m.Votes)
                                        .Include(m => m.Flags)
                                        .Include(m => m.Responses)
                                        .Include(m => m.AppUser);
            return await msg.OrderByDescending(d => d.DateStamp).ToListAsync();
        }

        // GET: api/MyMessages ***ALL MESSAGES BY CURRENT USER***
        [HttpGet("MyMessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMyMessages()
        {
            var currentUser = (User)HttpContext.Items["User"];
            var msg = _context.Message.Where(my => my.AppUser == currentUser)
                                        .Include(m => m.Flags)
                                        .Include(m => m.Responses)
                                        .Include(m => m.Votes);
            return await msg.ToListAsync();
        }

        // GET: api/UserMessages ***ALL MESSAGES BY USER ID***
        [HttpGet("UserMessages/{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetUserMessages(int id)
        {

            var msg = _context.Message.Include(m => m.Votes)
                                        .Include(m => m.Flags)
                                        .Include(m => m.Responses)
                                        .Where(my => my.AppUser.Id == id);
            return await msg.ToListAsync();
        }

        // GET: api/UserMessages ***ALL MESSAGES BY FOLLOWED USERS ID***
        [HttpGet("FollowingUserMessages/")]
        public async Task<ActionResult<IList<Message>>> GetFollowingUserMessages()
        {
            var currentUser = (User)HttpContext.Items["User"];
            var fUsers = _context.FollowingUsers;
            var id = currentUser.Id;

            var listOfFollowers =
                        from f in _context.FollowingUsers
                        join u in _context.Users
                            on f.AppUser.Id equals u.Id
                        where u.Id == id
                        select f.FollowingUserId;
            var msg =
                        from m in _context.Message
                        where listOfFollowers.Contains(m.AppUser.Id)
                        select m;

            var message = msg.Include(m => m.Flags)
                                .Include(m => m.AppUser)
                                .Include(m => m.Responses)
                                .Include(m => m.Votes);
            return await message.ToListAsync();
        }


        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            // var message = await _context.Message.FindAsync(id);
            var message = await _context.Message.Include(m => m.Votes)
                                        .Include(m => m.Flags)
                                        .Include(m => m.Responses)
                                        .Include(m => m.AppUser)
                                        .SingleOrDefaultAsync(i => i.MessageId == id);

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

            message.DateStamp = DateTime.Now;
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
            message.AppUser = currentUser;
            message.DateStamp = DateTime.Now;
            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.MessageId == id);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~VOTING HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // GET: api/Votes/5
        [HttpGet("Votes/{id}")]
        public async Task<ActionResult<Vote>> GetVote(int id)
        {
            var vote = await _context.Vote.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }


        // POST: api/Votes set vote // this is for if they can submit a 1 or -1 on front end as voteValue for Messages
        // no JSON raw data is required
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("voteMessage/{id}")]
        public async Task<ActionResult<Vote>> PostMessageVote(int id, int voteValue, Vote vote)
        {
            vote.Value = voteValue;
            var currentUser = (User)HttpContext.Items["User"];
            vote.AppUser = currentUser;
            vote.MessageId = id;
            _context.Vote.Add(vote);
            await _context.SaveChangesAsync();

            //summation query
            var sumVotes = _context.Vote.Where(v => v.MessageId == id).Sum(v => v.Value); //get summation
            var queryMsg = _context.Message.Single(m => m.MessageId == id); //retrieve message record for update

            queryMsg.VoteSummary = sumVotes;
            _context.Entry(queryMsg).State = EntityState.Modified;
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetVote", new { id = vote.VoteId }, vote);
        }


        // POST: api/Votes UPVOTE // this is for if they can submit a 1 or -1 on front end as voteValue for Responses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("voteResponse/{id}")]
        public async Task<ActionResult<Vote>> PostResponseVote(int id, int voteValue, Vote vote)
        {
            vote.Value = voteValue;
            var currentUser = (User)HttpContext.Items["User"];
            vote.AppUser = currentUser;
            vote.ResponseId = id;
            _context.Vote.Add(vote);
            await _context.SaveChangesAsync();

            //summation query
            var sumVotes = _context.Vote.Where(v => v.ResponseId == id).Sum(v => v.Value); //get summation
            var queryRes = _context.Responses.Single(m => m.ResponseId == id); //retrieve message record for update

            queryRes.VoteSummary = sumVotes;
            _context.Entry(queryRes).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetVote", new { id = vote.VoteId }, vote);
        }

        // make delete vote for responses and messages, then add summation into the method
        // DELETE: api/Votes/5
        [HttpDelete("voteMessage/{id}")]
        public async Task<IActionResult> DeleteVoteMess(int id)
        {
            // var voteid = _context.Vote.Where(v=>v.MessageId==id);
            var vote = await _context.Vote.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            var msgId = vote.MessageId;
            _context.Vote.Remove(vote);
            await _context.SaveChangesAsync();

            //summation query
            var sumVotes = _context.Vote.Where(v => v.MessageId == msgId).Sum(v => v.Value); //get summation
            var queryMsg = _context.Message.Single(m => m.MessageId == msgId); //retrieve message record for update

            queryMsg.VoteSummary = sumVotes;
            _context.Entry(queryMsg).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // make delete vote for responses and messages, then add summation into the method
        // DELETE: api/Votes/5
        [HttpDelete("voteResponse/{id}")]
        public async Task<IActionResult> DeleteVoteResp(int id)
        {
            var vote = await _context.Vote.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            var resId = vote.ResponseId;
            _context.Vote.Remove(vote);
            await _context.SaveChangesAsync();

            //summation query
            var sumVotes = _context.Vote.Where(v => v.ResponseId == resId).Sum(v => v.Value); //get summation
            var queryRes = _context.Responses.Single(m => m.ResponseId == resId); //retrieve message record for update

            queryRes.VoteSummary = sumVotes;
            _context.Entry(queryRes).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool VoteExists(int id)
        {
            return _context.Vote.Any(e => e.VoteId == id);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~FOLLOWING USERS HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        // GET: api/followingUsers
        [HttpGet("followingUsers/")]
        public async Task<ActionResult<IEnumerable<FollowingUser>>> GetFollowingUsers()
        {
            JsonSerializerOptions options = new()
            {
                //switch to Always to ignore
                DefaultIgnoreCondition = JsonIgnoreCondition.Never
            };
            var following = await _context.FollowingUsers
                                            .Include(f => f.AppUser)
                                            .ToListAsync();
            return following;
        }

        // POST: api/FollowingUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("followingUsers/{id}")]
        public async Task<ActionResult<FollowingUser>> PostVote(int id, FollowingUser followingUser)
        {

            var currentUser = (User)HttpContext.Items["User"];
            followingUser.AppUser = currentUser;
            followingUser.FollowingUserId = id;
            _context.FollowingUsers.Add(followingUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowingUsers", new { id = followingUser.AppUser }, followingUser);
        }

        // DELETE: api/followingUsers/5
        [HttpDelete("followingUsers/{id}")]
        public async Task<IActionResult> DeleteFollowingUser(int id)
        {
            var followingUser = await _context.FollowingUsers.FindAsync(id);
            if (followingUser == null)
            {
                return NotFound();
            }

            _context.FollowingUsers.Remove(followingUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowingUserExists(int id)
        {
            return _context.FollowingUsers.Any(e => e.Id == id);
        }



    }
}
