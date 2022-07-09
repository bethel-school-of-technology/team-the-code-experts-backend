using System.Text.Json.Serialization;
using WebApi.Entities;
namespace Broadcast_JWT.Models;
public class Message
{
    public int MessageId { get; set; }
    // public User AppUser { get; set; }
    public int UserId { get; set; }
    public DateTime DateStamp { get; set; }
    public string MessageTitle { get; set; }
    public string MessageBody { get; set; }
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  //Ignore for POST activities - Alternative may be JsonIncludeAttribute
    public IEnumerable<Vote> Votes { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  //Ignore for POST activities
    public IEnumerable<Flag> Flags { get; set; }


}
