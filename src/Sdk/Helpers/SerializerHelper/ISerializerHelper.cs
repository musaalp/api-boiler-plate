namespace Sdk.Helpers.SerializerHelper
{
    public interface ISerializerHelper
    {
        string Serialize(object obj);
        T Deserialize<T>(string value);
    }
}
