using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Core.Data.Handlers
{
internal static class SerializationHelper
{
    private static readonly JsonSerializerSettings Settings = new()
    {
        Converters = new List<JsonConverter>
        {
            new TypedGameDataJsonConverter()
        }
    };
    
    public static T DeserializeFromJson<T>(byte[] dataBytes)
    {
        var jsonString = Encoding.UTF8.GetString(dataBytes);
        return JsonConvert.DeserializeObject<T>(jsonString, Settings);
    }

    public static string SerializeToJson<T>(T data)
    {
        return JsonConvert.SerializeObject(data, Settings);
    }
}
}