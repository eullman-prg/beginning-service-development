using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevelopersAPI.Domain
{
    public class DeveloperEntity
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public bool IsOnCallDeveloper { get; set; } = false;

    }
}
