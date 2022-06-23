using Microsoft.AspNetCore.Identity;

namespace Broadcast.Models
{
    public class AppUser : IdentityUser {
        public int UserId {get; set;}
        public IEnumerable<FollowingUser> FollowingUsers {get; set;} //AppUser will be one to many FUserIds
        public IEnumerable<Message> Messages {get; set;} //AppUser will be one to many Messages
        public bool Admin {get; set;}

    }
}