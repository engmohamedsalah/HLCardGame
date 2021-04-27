using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HLCardGame.Infrastructure.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardColor
    {
        [EnumMember(Value = "Black")]
        Black,

        [EnumMember(Value = "Red")]
        Red
    }
}