using WebApi.Entities;

namespace Broadcast_JWT.Models;

public class FollowingUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FollowingUserId { get; set; } //Following Users

}