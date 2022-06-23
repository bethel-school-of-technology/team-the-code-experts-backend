namespace Broadcast.Models
{
    public class Vote {
        public int VoteId {get; set;}
        public Message Message {get; set;}
        public int UserId {get; set;}
        public bool UpVote {get; set;}
        public bool DownVote {get; set;}

    }
}