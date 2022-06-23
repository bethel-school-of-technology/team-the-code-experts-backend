namespace Broadcast.Models
{
    public class Message {
        public int MessageId {get; set;}
        public AppUser User {get; set;}
        public DateTime DateStamp {get; set;}
        public string MessageTitle {get; set;}
        public string MessageBody {get; set;}
        public IEnumerable<Vote> Votes {get; set;}
        public IEnumerable<Flag> Flags {get; set;}


    }
}