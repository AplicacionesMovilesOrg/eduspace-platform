using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Serializers;

/// <summary>
///     Custom BSON serializer for DateOnly type
///     Serializes DateOnly as a UTC DateTime in MongoDB
/// </summary>
public class DateOnlySerializer : SerializerBase<DateOnly>
{
    public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();

        return bsonType switch
        {
            BsonType.DateTime => DateOnly.FromDateTime(BsonSerializer.Deserialize<DateTime>(context.Reader)),
            BsonType.String => DateOnly.Parse(context.Reader.ReadString()),
            _ => throw new NotSupportedException($"Cannot deserialize BsonType {bsonType} to DateOnly")
        };
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
    {
        var dateTime = value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        context.Writer.WriteDateTime(new BsonDateTime(dateTime).MillisecondsSinceEpoch);
    }
}