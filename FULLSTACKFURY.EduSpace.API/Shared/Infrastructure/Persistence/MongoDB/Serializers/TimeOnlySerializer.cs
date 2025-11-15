using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.MongoDB.Serializers;

/// <summary>
///     Custom BSON serializer for TimeOnly type
///     Serializes TimeOnly as total ticks (long) in MongoDB
/// </summary>
public class TimeOnlySerializer : SerializerBase<TimeOnly>
{
    public override TimeOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();

        return bsonType switch
        {
            BsonType.Int64 => TimeOnly.FromTimeSpan(TimeSpan.FromTicks(context.Reader.ReadInt64())),
            BsonType.String => TimeOnly.Parse(context.Reader.ReadString()),
            _ => throw new NotSupportedException($"Cannot deserialize BsonType {bsonType} to TimeOnly")
        };
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeOnly value)
    {
        context.Writer.WriteInt64(value.ToTimeSpan().Ticks);
    }
}