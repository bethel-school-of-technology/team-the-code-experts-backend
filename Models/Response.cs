namespace Broadcast_JWT.Models;

using System.Text.Json.Serialization;
using WebApi.Entities;
public class Response
{
    public int ResponseId { get; set; }
    public int MessageId { get; set; }
    public int FlagId {get; set;}
   [JsonIgnore(Condition = JsonIgnoreCondition.Always)]  //Ignore for POST activities - Alternative may be JsonIncludeAttribute
    public IEnumerable<Vote> Votes { get; set; }
    public User AppUser { get; set; }
}