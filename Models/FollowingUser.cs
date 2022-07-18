using System.Text.Json.Serialization;
using WebApi.Entities;

namespace Broadcast_JWT.Models;

public class FollowingUser
{
    public int Id { get; set; }
    // public int UserId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]  //Ignore for POST activities
    // public int AppUserId { get; set; }
    public User AppUser {get; set;}
    public int FollowingUserId { get; set; } //Following Users

}