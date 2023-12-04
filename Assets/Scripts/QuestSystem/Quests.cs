using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Quests
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum QuestStatus
    {
        NotStarted, InProgress, Finished, Blocked, Completed
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubTaskType
    {
        Gather, Craft, Fetch, Interaction, Train
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum QuestType
    {
        Simultaneous, InOrder, Either
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResourceTag
    {
        Currency,
        Resource_Wood, Resource_Rock, Resource_Iron, Resource_Ore, Resource_Gem, Resource_Food, Resource_Bone, Resource_Shell,
        Resource_Sand, Resource_Clay,
        Item_Forge, Item_IronArmor, Item_ClayPot, Item_Crystal,
        Tool_Pickaxe, Tool_Kiln, Tool_Scoop,
        WorldTree,
        Train_Stamina, Train_Strength, Train_Speed,
        NPC
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CharacterTag
    {
        Giver, Receiver, GiverReceiver, Interactable
    }
}