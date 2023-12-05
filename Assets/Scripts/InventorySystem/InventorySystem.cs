using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;
using UnityEngine.Events;
using AYellowpaper.SerializedCollections;
using Inventory;

[CreateAssetMenu(menuName = "Systems/InventorySystem")]
public class InventorySystem : ScriptableObject
{
    public QuestSystem qs;
    public SerializedDictionary<GameItem, int> inventory;
    public UnityAction<GameItem, int> OnitemAdded;
    public UnityAction<GameItem, int> OnitemRemoved;

    public int MoneyInTheBank;

    void OnEnable()
    {
        //inventory = new SerializedDictionary<GameItem, int>();
        qs.QuestAddedEvent += CheckProgressOnNewQuest;
        qs.QuestRemovedEvent += ReceiveRewards;
    }

    void CheckProgressOnNewQuest(QuestData quest)
    {
        foreach (SubTask subTask in quest.QuestList)
        {
            if (subTask.type == SubTaskType.Gather || subTask.type == SubTaskType.Train)
                return;

            GameItem i = SearchByResourceTag(subTask.resourceTarget);
            if (i)
            {
                quest.AddProgress(subTask.resourceTarget, inventory[i]);
            }
        }
    }

    public void AddToInventory(GameItem item, int value, bool trackable = true)
    {
        if (item.health == -1 && item.type == TypeOfObject.QuestObjects)
        {
            string id = item.id.Trim("QuestItem".ToCharArray());
            QuestData data = Resources.Load<QuestData>("Data/Quests/" + id);
            qs.AddQuest(data);
            return;
        }

        if (inventory.ContainsKey(item))
        {
            inventory[item] += value;
            Debug.Log("inventory has key: Add +1");
        }
        else
        {
            Debug.Log("inventory didn't have key: Add item");
            inventory.Add(item, value);
        }

        if (trackable)
            qs.AddProgress(item.tag, value);

        OnitemAdded?.Invoke(item, value);
    }
    //if value = -1 remove it regardless
    public void RemoveFromInventory(GameItem item, int value)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= value;
            OnitemRemoved?.Invoke(item, value);
            if (value == -1 || inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
        else
        {
            Debug.Log($"{item.name} does not exist in the inventory");
        }
    }

    public void RemoveFromInventory(string itemId, int value)
    {
        GameItem item = null;
        foreach (var kv in inventory)
        {
            if (kv.Key.id == itemId)
            {
                item = kv.Key;
                break;
            }
        }
        if (item)
        {
            inventory[item] -= value;
            OnitemRemoved?.Invoke(item, value);
            if (value == -1 || inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
        else
        {
            Debug.Log($"{item.name} does not exist in the inventory");
        }
    }

    public void ReceiveRewards(QuestData data)
    {
        if (data.status == QuestStatus.Finished)
        {
            foreach (var kv in data.Rewards)
            {
                AddToInventory(kv.Key, kv.Value);
            }
        }
        else Debug.Log("Quest Not Finished");
    }

    public void SpendResources(QuestData data)
    {
        if (data.resourceSpent)
        {
            foreach (SubTask subTask in data.QuestList)
            {
                //find the resource target
                var keys = new List<GameItem>(inventory.Keys);
                foreach (var k in keys)
                {
                    if (k.tag == subTask.resourceTarget)
                    {
                        inventory[k] = inventory[k] - subTask.value;
                        break;
                    }
                }
            }
        }
        else Debug.Log("InventorySystem: Resources not spent");
    }

    public GameItem SearchByResourceTag(ResourceTag tag)
    {
        foreach (var kv in inventory)
        {
            if (kv.Key.tag == tag)
            {
                return kv.Key;
            }
        }
        return null;
    }

    public int Contains(GameItem item)
    {
        if (inventory.ContainsKey(item))
        {
            return inventory[item];
        }
        return -1;
    }

    public int Contains(string item)
    {
        foreach (var k in inventory.Keys)
        {
            if (k.id == item)
                return inventory[k];
        }
        return -1;
    }
}
