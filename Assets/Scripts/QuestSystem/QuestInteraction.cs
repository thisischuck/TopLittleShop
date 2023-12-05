using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;
using Inventory;

public class QuestInteraction : MonoBehaviour
{
    public string id;
    public ResourceTag resourceTag;
    public CharacterTag characterTag;
    public QuestSystem questSystem;
    public InventorySystem inventorySystem;

    [SerializeField] Vector3 InitialScale;

    //Complete and remove the quest on called
    public void AddOrCompleteQuest()
    {
        if (!questSystem.currentFlow.Quest)
        {
            return;
        }

        switch (questSystem.currentFlow.Quest.status)
        {
            case QuestStatus.Finished:
                if (characterTag.Equals(CharacterTag.Giver))
                    break;
                if (id == questSystem.currentFlow.Quest.ReceiverId)
                    CompleteQuest(questSystem.currentFlow);
                break;
            case QuestStatus.NotStarted:
                if (characterTag.Equals(CharacterTag.Receiver))
                    break;
                if (id == questSystem.currentFlow.Quest.GiverId)
                {
                    AddQuest(questSystem.currentFlow.Quest);
                    Debug.Log(questSystem.currentFlow.Quest.id + " " + id);
                }
                break;
        }
    }

    void CompleteQuest(QuestFlow flow)
    {
        Debug.Log("Completed Quest: ", flow.Quest);
    }

    void AddQuest(QuestData quest)
    {
        Debug.Log("Added Quest: ", quest);
    }

    void Start()
    {
        InitialScale = transform.localScale;
    }

    private void OnEnable()
    {
        questSystem.QuestAddedEvent += AddQuest;
    }

    private void OnDisable()
    {
        questSystem.QuestAddedEvent -= AddQuest;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x > transform.position.x)
            {
                Vector3 lookLeftScale = InitialScale;
                lookLeftScale.x = InitialScale.x * -1;
                transform.localScale = lookLeftScale;
            }
            else
            {
                transform.localScale = InitialScale;
            }

        }
    }


}
