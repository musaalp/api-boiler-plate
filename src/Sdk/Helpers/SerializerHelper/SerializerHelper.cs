using Newtonsoft.Json;

namespace Sdk.Helpers.SerializerHelper
{
    public class SerializerHelper : ISerializerHelper
    {
        public string Serialize(object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
