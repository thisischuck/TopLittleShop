using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using Quests;
using Newtonsoft.Json;
using MyJsonConverters;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "Data/Quests/QuestData")]
public class QuestData : ScriptableObject
{
    //! What happens if 2 quests have the same miniQuest?
    //! Maybe it needs to be two seperate instances of the same ScObj
    //! FIGURE THIS OUT

    public string id;
    public QuestType type;

    [JsonConverter(typeof(SubTaskJsonConveter))]
    public List<SubTask> QuestList;

    /// <summary>
    /// The Quest giver and receiver. 
    /// If receiver is null you need to go back to the giver.
    /// </summary>
    public string GiverId, ReceiverId;
    public string QuestName;
    public bool resourceSpent;
    public QuestStatus status;
    [JsonConverter(typeof(RewardsJsonConveter))]
    public SerializedDictionary<GameItem, int> Rewards;
    [TextArea]
    public string description;

    public DialogData Dialog;

    [JsonIgnore]
    public UnityAction<string> Finished, Progress, Started;

    public void StartQuest()
    {
        status = QuestStatus.InProgress;
        Started?.Invoke(id);
        if (type == QuestType.Simultaneous || type == QuestType.Either)
        {
            QuestList.ForEach((x) => x.status = QuestStatus.InProgress);
        }
        else
        {
            QuestList[0].status = QuestStatus.InProgress;
        }
    }

    public void NextQuest()
    {
        if (type == QuestType.InOrder)
        {
            foreach (SubTask q in QuestList)
            {
                if (q.status == QuestStatus.Blocked)
                {
                    q.SetStatus(QuestStatus.InProgress);
                    break;
                }
            }
            CheckFinish();
        }
    }

    public int AddProgress(ResourceTag tag, int value)
    {
        foreach (SubTask m in QuestList)
        {
            if (m.status == QuestStatus.InProgress && m.resourceTarget == tag)
            {
                m.AddProgress(value);
                //Because of script execution order. Scriptable objects happen first than UI;
                Progress?.Invoke(id);
                CheckFinish();
                return 0;
            }
        }
        return 1;
    }

    public void CheckFinish()
    {
        if (type == QuestType.Either)
        {
            foreach (SubTask q in QuestList)
            {
                if (q.status == QuestStatus.Finished)
                {
                    status = QuestStatus.Finished;
                    Finished?.Invoke(id);
                    return;
                }
            }
        }
        else
        {
            foreach (SubTask q in QuestList)
            {
                if (q.status != QuestStatus.Finished)
                {
                    return;
                }
            }
            status = QuestStatus.Finished;
            Finished?.Invoke(id);
        }
    }

    public void ResetQuest()
    {
        //Resets the quest in case the user wants to pick it up again;
        //Loopable Quests
    }

    public override string ToString()
    {
        string s = "";
        if (status == QuestStatus.Finished)
        {
            s = name + ": Finished";
        }
        else
        {
            foreach (SubTask q in QuestList)
            {
                s += q.ToString();
                s += "\n";
            }
        }

        return s;
    }

    void OnDisable()
    {
        status = QuestStatus.NotStarted;
    }

    public void ForceFinish()
    {
        status = QuestStatus.Finished;
        Finished?.Invoke(id);
    }

    public static QuestData Create(QuestData i)
    {
        string pathName = i.id + ".asset";
        if (File.Exists("Resources/Data/Quests/" + pathName)) return null;

        QuestData item = CreateInstance<QuestData>();
        item.id = i.id;
        item.QuestName = i.QuestName;
        item.Rewards = i.Rewards;
        item.QuestList = i.QuestList;
        item.type = i.type;
        item.status = i.status;
        item.description = i.description;
        item.resourceSpent = i.resourceSpent;
        item.GiverId = i.GiverId;
        item.ReceiverId = i.ReceiverId;
        Debug.Log(pathName);
        AssetUtils.CreateAsset<QuestData>("Assets/Resources/Data/Quests/" + pathName, item);
        return item;
    }
}

