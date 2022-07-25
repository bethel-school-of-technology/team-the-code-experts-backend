namespace Broadcast_JWT.Models;
using WebApi.Entities;
public class Flag
{
    public int FlagId { get; set; }
    public int? MessageId { get; set; } // incase this is the parent message, this will be null
    public int? ResponseId { get; set; } // incase this is the parent message, this will be null
    public int ReasonId { get; set; }
    public User AppUser { get; set; }



}