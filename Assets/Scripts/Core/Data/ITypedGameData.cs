using Newtonsoft.Json;

namespace Core.Data
{
internal interface ITypedGameData
{
    [JsonProperty(TYPE_FIELD_NAME, Order = -999)]
    public string Type { get; set; }
    
    public const string TYPE_FIELD_NAME = "type";
}
}