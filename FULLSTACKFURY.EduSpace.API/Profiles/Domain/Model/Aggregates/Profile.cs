using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;

public class Profile
{
    public Profile(string firstName, string lastName
        , string email, string dni, string address
        , string phone, AccountId accountId)
    {
        ProfileName = new ProfileName(firstName, lastName);
        ProfilePrivateInformation = new ProfilePrivateInformation(email, dni, address, phone);
        AccountId = accountId;
    }

    public Profile()
    {
        ProfileName = new ProfileName();
        ProfilePrivateInformation = new ProfilePrivateInformation();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("profile_name")] public ProfileName ProfileName { get; set; }

    [BsonElement("profile_private_information")]
    public ProfilePrivateInformation ProfilePrivateInformation { get; set; }

    [BsonElement("account_id")] public AccountId AccountId { get; private set; }

    [BsonElement("created_date")] public DateTimeOffset? CreatedDate { get; set; }

    [BsonElement("updated_date")] public DateTimeOffset? UpdatedDate { get; set; }

    public string ProfileFullName => ProfileName.FullName;
    public string ProfileEmail => ProfilePrivateInformation.ObtainEmail;
    public string ProfileDni => ProfilePrivateInformation.ObtainDni;
    public void UpdateInformation(
        string firstName,
        string lastName,
        string email,
        string dni,
        string address,
        string phone)
    {
        ProfileName = new ProfileName(firstName, lastName);
        ProfilePrivateInformation = new ProfilePrivateInformation(email, dni, address, phone);
        UpdatedDate = DateTimeOffset.UtcNow;
    }

}