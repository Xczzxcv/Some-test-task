using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Data.Handlers
{
internal abstract class TypedGameDataJsonConverter<TData> : JsonConverter<TData>
    where TData : IGameDataPiece, ITypedGameData
{
    private readonly Dictionary<Type, string> _typeToStr = new();
    private readonly Dictionary<string, Type> _strToType = new();

    protected TypedGameDataJsonConverter()
    {
        FillMapDictionaries();
    }

    private void FillMapDictionaries()
    {
        var mapEntities = GetMapEntities();
        foreach (var (str, type) in mapEntities)
        {
            _strToType.Add(str, type);
            _typeToStr.Add(type, str);
        }
    }

    protected abstract (string, Type)[] GetMapEntities();

    public override void WriteJson(JsonWriter writer, TData value, JsonSerializer serializer)
    {
        if (!_typeToStr.TryGetValue(value.GetType(), out var typeStr))
        {
            throw new ArgumentException($"Can't find type string for object '{value.GetType()}'");
        }

        value.Type = typeStr;
        var jObject = JObject.FromObject(value);
        jObject.WriteTo(writer);
    }

    public override TData ReadJson(JsonReader reader, Type objectType,
        TData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jToken = JToken.ReadFrom(reader);
        if (jToken.Type == JTokenType.Null)
        {
            return default;
        }

        var typeStr = jToken[ITypedGameData.TYPE_FIELD_NAME]?.ToString();
        if (typeStr == null)
        {
            throw new ArgumentException(
                $"Can't parse object with empty/nonexistent field '{ITypedGameData.TYPE_FIELD_NAME}'");
        }

        if (!_strToType.TryGetValue(typeStr, out var type))
        {
            throw new ArgumentException($"Can't parse type of object '{typeStr}'");
        }

        return (TData) jToken.ToObject(type);
    }
}
}