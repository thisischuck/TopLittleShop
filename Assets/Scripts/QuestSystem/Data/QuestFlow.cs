using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Data/Quests/Flow")]
public class QuestFlow : ScriptableObject
{
    public List<QuestData> flow;
    public int position;
    public bool finished = false;

    public UnityAction<QuestData> OnNextQuest;

    public QuestData Quest
    {
        get { return flow[position]; }
    }

    public void Next()
    {
        position += 1;
        if (position >= flow.Count)
        {
            finished = true;
        }
        else
        {
            OnNextQuest.Invoke(Quest);
        }
    }

    void OnDisable()
    {
        finished = false;
        position = 0;
    }

}
