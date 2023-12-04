using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Quests;
using Newtonsoft.Json;
using System.IO;
using MyJsonConverters;

[CreateAssetMenu(menuName = "Data/Quests/SubTasks")]
public class SubTask : ScriptableObject
{
    public string id;
    public SubTaskType type;
    public QuestStatus status;

    /// <summary>
    /// The Quest giver and receiver. 
    /// If receiver is null you need to go back to the giver.
    /// </summary>
    public string tagId;

    public string QuestName;
    public ResourceTag resourceTarget;
    public bool resourceSpent;
    public int currentValue = 0;
    public int value = 5;
    [TextArea]
    public string description;

    [JsonIgnore]
    public UnityAction<string> Progress, Finished;

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        status = QuestStatus.NotStarted;
        currentValue = 0;
    }

    public void SetStatus(QuestStatus s)
    {
        status = s;
    }

    public void AddProgress(int n)
    {
        currentValue += n;
        Debug.Log(currentValue);
        //Progress.Invoke(id);
        Finish();
    }

    public void Finish()
    {
        if (currentValue < value)
            return;

        status = QuestStatus.Finished;
        Debug.Log(status);
        //Finished.Invoke();
    }

    public override string ToString()
    {
        return $"{QuestName}:\n{resourceTarget}:{currentValue}/{value}";
    }

    public static SubTask Create(SubTask i)
    {
        string pathName = i.id + ".asset";
        if (File.Exists("Resources/Data/Quests/SubTasks/" + pathName)) return null;

        SubTask item = CreateInstance<SubTask>();
        item.id = i.id;
        item.QuestName = i.QuestName;
        item.type = i.type;
        item.status = i.status;
        item.description = i.description;
        item.resourceSpent = i.resourceSpent;
        item.currentValue = i.currentValue;
        item.value = i.value;
        item.tagId = i.tagId;
        AssetUtils.CreateAsset<SubTask>("Assets/Resources/Data/Quests/SubTasks/" + pathName, item);
        return item;
    }
}
