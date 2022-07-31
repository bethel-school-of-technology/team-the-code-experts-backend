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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MESSAGE HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // GET: api/Messages                                ***ALL MESSAGES IN NEWEST POST (DESCENDING) ORDER***
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var currentUser = (User)HttpContext.Items["User"];
            var id = currentUser.Id;

            var msg = _context.Message.Include(m => m.AppUser)
                                .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                .Include(m => m.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.AppUser)
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == id))
                                .Include(f => f.AppUser)
                                .ThenInclude(f => f.FollowingUsers)
                                .OrderByDescending(m => m.DateStamp);

            return await msg.ToListAsync();
        }

        // GET: api/Messages/Flagged                         ***ALL FLAGGED MESSAGES IN OLDEST POST (ASCENDING - TO DELETE IN ORDER RECEIVED) ORDER***
        [HttpGet("Flagged")]
        public async Task<ActionResult<IEnumerable<Message>>> GetFlaggedMessages()
        {
            var currentUser = (User)HttpContext.Items["User"];
            var id = currentUser.Id;

            var msg = _context.Message.Include(m => m.AppUser)
                                .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                .Include(m => m.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.AppUser)
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == id))
                                .Include(f => f.AppUser)
                                .ThenInclude(f => f.FollowingUsers)
                                //Only include populated Flagged
                                .Where(m => m.Flags != null && m.Flags.Any())
                                .OrderBy(m => m.DateStamp);
            return await msg.ToListAsync();
        }

        // GET: api/Messages/Ascending                               ***ALL MESSAGES IN OLDEST POST (ASCENDING) DATE ORDER***
        [HttpGet("Ascending")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesDesc()
        {
            var currentUser = (User)HttpContext.Items["User"];
            var id = currentUser.Id;

            var msg = _context.Message.Include(m => m.AppUser)
                                .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                .Include(m => m.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.AppUser)
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == id))
                                .Include(f => f.AppUser)
                                .ThenInclude(f => f.FollowingUsers);
            return await msg.OrderBy(d => d.DateStamp).ToListAsync();
        }

        // GET: api/Messages/MyMessages                              ***ALL MESSAGES BY CURRENT USER IN NEWEST POST ORDER***
        [HttpGet("MyMessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMyMessages()
        {
            var currentUser = (User)HttpContext.Items["User"];
            var id = currentUser.Id;

            var msg = _context.Message.Where(my => my.AppUser == currentUser)
                                        .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                        .Include(m => m.Votes.Where(v => v.AppUser.Id == id))
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.AppUser)
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == id))
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == id))
                                        .Include(f => f.AppUser)
                                        .ThenInclude(f => f.FollowingUsers)
                                        .OrderByDescending(m => m.DateStamp);

            return await msg.ToListAsync();
        }

        // GET: api//Messages/UserMessages                            ***ALL MESSAGES IN NEWEST POST ORDER BY USER ID***
        [HttpGet("UserMessages/{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetUserMessages(int id)
        {

            var currentUser = (User)HttpContext.Items["User"];
            var cid = currentUser.Id;

            var msg = _context.Message.Include(m => m.AppUser)
                                        .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                        .Include(m => m.Votes.Where(v => v.AppUser.Id == cid))
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.AppUser)
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == cid))
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == cid))
                                        .Include(f => f.AppUser)
                                        .ThenInclude(f => f.FollowingUsers)
                                        .Where(my => my.AppUser.Id == id)
                                        .OrderByDescending(m => m.DateStamp);
            return await msg.ToListAsync();
        }

        // GET: api/Messages/FollowingMessages                      ***ALL MESSAGES IN NEWEST POST ORDER BY FOLLOWED USERS ID***
        [HttpGet("FollowingMessages/")]
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

            var message = msg.Include(m => m.AppUser)
                                .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                .Include(m => m.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.AppUser)
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == id))
                                .Include(r => r.Responses)
                                .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == id))
                                .Include(f => f.AppUser)
                                .ThenInclude(f => f.FollowingUsers)
                                .OrderByDescending(d => d.DateStamp);
            return await message.ToListAsync();
        }


        // GET: api/Messages/5                             ***SPECIFIC MESSAGES BY ID***
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var cid = currentUser.Id;

            var msg = _context.Message.Include(m => m.AppUser)
                                        .Include(f => f.Flags.Where(f => f.AppUser.Id == id))
                                        .Include(m => m.Votes.Where(v => v.AppUser.Id == cid))
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.AppUser)
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.Votes.Where(v => v.AppUser.Id == cid))
                                        .Include(r => r.Responses)
                                        .ThenInclude(r => r.Flags.Where(f => f.AppUser.Id == cid))
                                        .Include(f => f.AppUser)
                                        .ThenInclude(f => f.FollowingUsers)
                                        .SingleOrDefaultAsync(i => i.MessageId == id);

            if (msg == null)
            {
                return NotFound();
            }

            return await msg;
        }


        // GET: api/Messages/Response/5                     ***SPECIFIC RESPONSES BY ID***
        [HttpGet("Response/{id}")]
        public async Task<ActionResult<Response>> GetResponse(int id)
        {


            var currentUser = (User)HttpContext.Items["User"];
            var cid = currentUser.Id;

            var response = await _context.Responses.Include(r => r.Votes.Where(v => v.AppUser.Id == cid))
                                                    .Include(r => r.Flags.Where(f => f.AppUser.Id == cid))
                                                    .Include(f => f.AppUser)
                                                    .ThenInclude(f => f.FollowingUsers)
                                                    .Include(r => r.AppUser)
                                                    .SingleOrDefaultAsync(i => i.ResponseId == id);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }


        // PUT: api/Messages/5                             ***EDIT MESSAGE***
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, [Bind("MessageId,UserId,DateStamp,MessageTitle,MessageBody")] Message message)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var cid = currentUser.Id;
            if (currentUser.Id != message.AppUser.Id)
            {
                return Unauthorized();
            }
            else
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



        // POST: api/Messages                             ***CREATE INITIAL MESSAGE***
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

        // POST: api/Messages/Response                     ***CREATE INITIAL RESPONSE***
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Response/{id}")]
        public async Task<ActionResult<Response>> PostResponse(Response response, int id)
        {
            JsonSerializerOptions options = new()
            {
                //switch to Always to ignore
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            response.MessageId = id;
            var currentUser = (User)HttpContext.Items["User"];
            response.AppUser = currentUser;
            response.DateStamp = DateTime.Now;
            _context.Responses.Add(response);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResponse", new { id = response.ResponseId }, response);
        }

        // DELETE: api/Messages/5                          ***DELETE MESSAGE BY ID***
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var message = await _context.Message.FindAsync(id);
            if (message.AppUser == currentUser || currentUser.Role == 1)
            {
                if (message == null)
                {
                    return NotFound();
                }

                _context.Message.Remove(message);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else return Unauthorized();
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.MessageId == id);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~VOTING HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // GET: api/Messages/Votes/5                               ***GET VOTE BY ID***
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


        // POST: api/Messages/VoteMessage/5 set vote                     ***CREATE VOTE FOR A MESSAGE***
        // this is for if they can submit a 1 or -1 on front end as voteValue for Messages
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


        // POST: api/Messages/VoteResponse/5                               ***CREATE VOTE BY ID FOR A RESPONSE***
        // this is for if they can submit a 1 or -1 on front end as voteValue for Responses
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


        // DELETE: api/Messages/VoteMessage/5                          ***DELETE VOTE BY ID FOR MESSAGE***
        // delete vote for responses and messages, then add summation into the method
        [HttpDelete("voteMessage/{id}")]
        public async Task<IActionResult> DeleteVoteMess(int id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var vote = await _context.Vote.FindAsync(id);
            if (vote.AppUser == currentUser || currentUser.Role == 1)
            {
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
            else return Unauthorized();
        }


        // DELETE: api/Messages/VoteResponse/5                            ***DELETE VOTE BY ID FOR RESPONSE***         
        // make delete vote for responses and messages, then add summation into the method
        [HttpDelete("voteResponse/{id}")]
        public async Task<IActionResult> DeleteVoteResp(int id)
        {

            var currentUser = (User)HttpContext.Items["User"];
            var vote = await _context.Vote.FindAsync(id);
            if (vote.AppUser == currentUser)
            {
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
            else return Unauthorized();
        }
        private bool VoteExists(int id)
        {
            return _context.Vote.Any(e => e.VoteId == id);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~FLAGS HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        // POST: api/Messages/MessageFlag/{id}                   **CREATE FLAG FOR MESSAGE***
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("MessageFlag/{id}")]
        public async Task<ActionResult<FollowingUser>> PostMessageFlag(int id, Flag flag)
        {

            var currentUser = (User)HttpContext.Items["User"];
            flag.AppUser = currentUser;
            flag.MessageId = id;
            _context.Flags.Add(flag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowingUsers", new { id = flag.AppUser }, flag);
        }


        // POST: api/Messages/ResponseFlag/{id}                 **CREATE FLAG FOR RESPONSE***
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ResponseFlag/{id}")]
        public async Task<ActionResult<FollowingUser>> PostResponseFlag(int id, Flag flag)
        {

            var currentUser = (User)HttpContext.Items["User"];
            flag.AppUser = currentUser;
            flag.ResponseId = id;
            _context.Flags.Add(flag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowingUsers", new { id = flag.AppUser }, flag);
        }

        // DELETE: api/Messages/flag/5                          **DELETE FLAG BY ID***
        [HttpDelete("flag/{id}")]
        public async Task<IActionResult> DeleteFlag(int id)


        {
            var currentUser = (User)HttpContext.Items["User"];
            var flag = await _context.Flags.FindAsync(id);

            //check - only original user or admin can delete
            if ((flag.AppUser == currentUser) || (currentUser.Role == 1))
            {
                if (flag == null)
                {
                    return NotFound();
                }

                _context.Flags.Remove(flag);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else return Unauthorized();
        }

        private bool FlagExists(int id)
        {
            return _context.Flags.Any(e => e.FlagId == id);
        }



        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~FOLLOWING USERS HTTP ACTIONS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        // GET: api/followingUsers                  ***GET FOLLOWING USERS OF CURRENT USER***
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

        // POST: api/Messages/FollowingUsers                    **CREATE FOLLOWING USERS FOR CURRENT USER***
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

        // DELETE: api/Messages/followingUsers/5                 **DELETE FOLLOWING USERS OF CURRENT USER***
        [HttpDelete("followingUsers/{id}")]
        public async Task<IActionResult> DeleteFollowingUser(int id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            var followingUser = await _context.FollowingUsers
                                                .Where(f => f.FollowingUserId == id && f.AppUser == currentUser)
                                                .SingleAsync();

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
