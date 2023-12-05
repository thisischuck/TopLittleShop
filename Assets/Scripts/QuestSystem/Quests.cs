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
    public enum CharacterTag
    {
        Giver, Receiver, GiverReceiver, Interactable
    }
}