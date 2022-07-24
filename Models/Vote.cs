using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Entities;

namespace Broadcast_JWT.Models;

public class Vote
{
    [Key]
    public int VoteId { get; set; }
    // public Message Message { get; set; }
    public int? MessageId { get; set; }
    public int? ResponseId { get; set; } // incase this is the parent message, this will be null
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public User AppUser { get; set; }

    public int Value { get; set; }

}
