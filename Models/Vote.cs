namespace Broadcast_JWT.Models;

// using WebApi.Entities;
public class Vote
{
    public int VoteId { get; set; }
    // public Message Message { get; set; }
    public int MessageId {get; set;}
    public int UserId { get; set; }
    // public User AppUser { get; set; }
    public bool UpVote { get; set; }
    public bool DownVote { get; set; }
}
