using MongoDB.Bson;
using Newtonsoft.Json;

namespace fl_students_api.Configurations
{
    public class ObjectIdConverter : JsonConverter<ObjectId>
    {
        public override void WriteJson(JsonWriter writer, ObjectId value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override ObjectId ReadJson(JsonReader reader, Type objectType, ObjectId existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return ObjectId.Parse(reader.Value.ToString()!);
        }
    }
}
