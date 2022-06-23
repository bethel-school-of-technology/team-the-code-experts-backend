namespace Broadcast.Models
{
    public class FollowingUser {
        public int Id {get; set;}
        public AppUser User {get; set;}
        public int FollowingUserId {get; set;} //Following Users

    }
}