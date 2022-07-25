namespace Broadcast_JWT.Models;

using System.Text.Json.Serialization;
using WebApi.Entities;
public class Response
{
    public int ResponseId { get; set; }
    public int MessageId { get; set; }
    public string ResponseBody { get; set; }
    public DateTime DateStamp { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  //Ignore for POST activities - Alternative may be JsonIncludeAttribute    
    public IEnumerable<Flag> Flags { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  //Ignore for POST activities - Alternative may be JsonIncludeAttribute
    public IEnumerable<Vote> Votes { get; set; }
    public User AppUser { get; set; }
    public int VoteSummary { get; set; }
}