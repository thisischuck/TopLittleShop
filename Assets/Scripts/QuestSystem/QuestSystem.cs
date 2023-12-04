using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;
using UnityEngine.Events;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Systems/QuestSystem")]
public class QuestSystem : ScriptableObject
{
    public QuestFlow currentFlow;
    public UnityAction<QuestData> QuestAddedEvent;
    public UnityAction<QuestData> QuestRemovedEvent;
    public List<QuestData> QuestList;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        QuestList.Clear();
    }

    public int AddProgress(ResourceTag questTag, int value)
    {
        foreach (QuestData q in QuestList)
        {
            int i = q.AddProgress(questTag, value);
            if (i == 0)
                return 0;
        }
        return 1;
    }

    public void Interaction(Event e, string b, string[] data)
    {
        if (e != null)
        {
            if (data == null)
                return;
            Debug.Log("QuestSystem: " + data);
            string id = data[0];
            foreach (QuestData q in QuestList)
            {
                foreach (SubTask s in q.QuestList)
                {
                    if (s.type == SubTaskType.Interaction)
                    {
                        if (s.tagId == id)
                        {
                            s.AddProgress(1);
                            q.Progress?.Invoke(q.id);
                            q.CheckFinish();
                        }
                    }
                }
            }
        }
    }

    public bool AddQuest(QuestData data)
    {
        if (QuestList.Contains(data)) return false;
        // Check if i already have the objects in my inventory
        // Not sure what the best way to do this is. 
        data.StartQuest();
        QuestList.Add(data);
        QuestAddedEvent.Invoke(data);
        return true;
    }

    public bool RemoveQuest(QuestData data)
    {
        if (!QuestList.Contains(data))
            return false;

        QuestRemovedEvent.Invoke(data);
        QuestList.Remove(data);
        return true;
    }
}
