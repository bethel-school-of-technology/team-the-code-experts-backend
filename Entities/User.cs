namespace WebApi.Entities;

using Broadcast_JWT.Models;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public int Role {get; set;}
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<FollowingUser> FollowingUsers { get; set; } //User will be one to many FUserIds
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public IEnumerable<Message> Messages { get; set; } //User will be one to many Messages
    [JsonIgnore]
    public string PasswordHash { get; set; }
}