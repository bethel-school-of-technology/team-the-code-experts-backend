namespace Broadcast_JWT.Models;
using WebApi.Entities;
public class Flag
{
    public int FlagId { get; set; }
    // public Message Message { get; set; }
    public int MessageId { get; set; }
    public int ReasonId {get; set;}
    public int UserId { get; set; }
    // public User AppUser { get; set; }



}