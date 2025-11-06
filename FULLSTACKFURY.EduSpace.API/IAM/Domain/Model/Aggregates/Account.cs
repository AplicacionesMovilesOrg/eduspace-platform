using System.Text.Json.Serialization;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;

public class Account
{
    public Account(string username, string passwordHash, string role)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = Enum.Parse<ERoles>(role);
    }

    public Account()
    {
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("username")] public string Username { get; private set; }

    [BsonElement("password_hash")]
    [JsonIgnore]
    public string PasswordHash { get; private set; }

    [BsonElement("role")]
    [BsonRepresentation(BsonType.String)]
    public ERoles Role { get; private set; }

    public Account UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    public Account UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public string GetRole()
    {
        return Role.ToString();
    }
}