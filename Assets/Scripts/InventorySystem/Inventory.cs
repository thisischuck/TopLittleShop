using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Inventory
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeOfObject
    {
        Goods, Resources, SpecialObjects, QuestObjects, Equipment
    };

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResourceTag
    {
        Currency, Resource, Item, Tool,
    }
}
