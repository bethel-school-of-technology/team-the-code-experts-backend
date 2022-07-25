
using System.ComponentModel.DataAnnotations;

namespace Broadcast_JWT.Models;

public class FlagType
{
    [Key]
    public int FlagTypeId { get; set; }
    public string Description { get; set; }

}
