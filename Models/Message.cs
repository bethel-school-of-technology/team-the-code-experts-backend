using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Entities;
namespace Broadcast_JWT.Models;
public class Message
{
    public int MessageId { get; set; }

    public User AppUser { get; set; }
    // public int AppUserId { get; set; }
    public DateTime DateStamp { get; set; }
    public string MessageTitle { get; set; }
    public string MessageBody { get; set; }
    public IEnumerable<Response> Responses { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  //Ignore for POST activities - Alternative may be JsonIncludeAttribute
    public IEnumerable<Vote> Votes { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  //Ignore for POST activities
    public IEnumerable<Flag> Flags { get; set; }
    public int VoteSummary { get; set; }


}
