using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity;
using UnityEngine;
using UnityEditor;
using AYellowpaper.SerializedCollections;
using System.IO;
using Inventory;
using System.Globalization;

namespace MyJsonConverters
{
    public class GameItemJsonConveter : JsonConverter<GameItem>
    {
        public override GameItem ReadJson(JsonReader reader, Type objectType, GameItem existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string assetPath = "Data/GameItems/" + existingValue.id;
            GameItem item = Resources.Load<GameItem>(assetPath);
            if (item != null)
            {
                Debug.LogError("File does not exist");
                return GameItem.Create(existingValue);
            }
            return item;
            //This creates a new game item. 
            //Probably not ideal but it's a solution;
        }

        public override void WriteJson(JsonWriter writer, GameItem value, JsonSerializer serializer)
        {
            Debug.Log(value.id);
            writer.WriteValue(value.id);
        }
    }

    public class SubTaskJsonConveter : JsonConverter<List<SubTask>>
    {
        public override List<SubTask> ReadJson(JsonReader reader, Type objectType, List<SubTask> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            List<SubTask> returnList = new List<SubTask>();
            JArray obj = JArray.Load(reader);
            foreach (string s in obj)
            {
                Debug.Log(s);
                string assetPath = "Data/Quests/MiniQuests/" + s;
                SubTask data = Resources.Load<SubTask>(assetPath);
                if (data != null)
                {
                    returnList.Add(data);
                }
                else Debug.LogError("File does not exist");
            }
            returnList.ForEach((x) => Debug.Log(x.ToString()));
            return returnList;
        }

        public override void WriteJson(JsonWriter writer, List<SubTask> value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var v in value)
            {
                writer.WriteValue(v.id);
            }
            writer.WriteEndArray();
        }
    }

    public class CraftRecipeJsonConveter : JsonConverter<CraftRecipe>
    {
        public override CraftRecipe ReadJson(JsonReader reader, Type objectType, CraftRecipe existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string assetPath = "Data/GameItems/Recipes/" + existingValue.id;
            if (!File.Exists(assetPath))
            {
                Debug.LogError("File does not exist");
                return CraftRecipe.Create(existingValue);
            }
            return Resources.Load<CraftRecipe>(assetPath);
            //This creates a new game item. 
            //Probably not ideal but it's a solution;
        }

        public override void WriteJson(JsonWriter writer, CraftRecipe value, JsonSerializer serializer)
        {
            Debug.Log(value.id);
            writer.WriteValue(value.id);
        }
    }

    public class RewardsJsonConveter : JsonConverter<SerializedDictionary<GameItem, int>>
    {
        public override SerializedDictionary<GameItem, int> ReadJson(JsonReader reader, Type objectType, SerializedDictionary<GameItem, int> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            SerializedDictionary<GameItem, int> dik = new SerializedDictionary<GameItem, int>();
            JObject obj = JObject.Load(reader);
            foreach (var kv in obj)
            {
                GameItem item;
                string assetPath = "Data/GameItems/" + kv.Key;
                if (((int)kv.Value) == -1)
                {
                    assetPath = "Data/GameItems/QuestItems/" + kv.Key + "QuestItem";
                    item = Resources.Load<GameItem>(assetPath);
                    if (item != null)
                    {
                        dik.Add(item, (int)kv.Value);
                    }
                    else
                    {
                        item = new GameItem();
                        item.type = TypeOfObject.QuestObjects;
                        item.id = kv.Key + "QuestItem";
                        item.health = (int)kv.Value;
                        dik.Add(GameItem.Create(item), (int)kv.Value);
                    }
                    Debug.Log("QuestItemAdded");
                    continue;
                }
                item = Resources.Load<GameItem>(assetPath);
                if (item != null)
                {
                    dik.Add(item, (int)kv.Value);
                }
                else Debug.LogError("File does not exist");
            }
            return dik;
        }

        public override void WriteJson(JsonWriter writer, SerializedDictionary<GameItem, int> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var v in value)
            {
                writer.WritePropertyName(v.Key.id);
                writer.WriteValue(v.Value);
            }
            writer.WriteEndObject();
        }
    }

    public class LineDataConverter : JsonConverter<LineData>
    {
        public override LineData ReadJson(JsonReader reader, Type objectType, LineData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            LineData item = new LineData();
            string line = (string)reader.Value;
            Debug.Log(line);
            string[] s = line.Split(':');
            foreach (string s2 in s)
            {
                Debug.Log(s2);
            }
            if (s.Length > 0)
            {
                item.talker = s[0];
                item.text = s[1];
                item.time = float.Parse(s[2]);
            }
            return item;
        }

        public override void WriteJson(JsonWriter writer, LineData value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.talker}:{value.text}:{value.time}");
        }
    }
}