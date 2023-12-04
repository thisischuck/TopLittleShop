using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quests;
using UnityEngine.AI;

public class DialogSystem : MonoBehaviour
{
    public QuestSystem questSystem;
    public InventorySystem inventorySystem;
    private QuestData _questData;
    private List<LineData> lines;
    public Transform textBackground;
    public TextMeshProUGUI textHolder;
    public Image Character, NPC;
    public GameObject nameplatePlayer, nameplateNPC;

    public Color Darken;

    private int line = -1;
    private bool canPlay = false;
    private bool isEnding = false;
    //We might need to change this
    private Vector3 textBackgroundStartScale = Vector3.one;

    public QuestData QuestData
    {
        get { return _questData; }
        set
        {
            _questData = value;
            if (_questData.Dialog)
            {
                canPlay = true;
                if (_questData.status == QuestStatus.Finished)
                {
                    isEnding = true;
                    lines = _questData.Dialog.CompletedLines;
                }
                else
                {
                    lines = _questData.Dialog.InitialLines;
                }
            }
            Debug.Log("DialogSystem: Received Data");
        }
    }

    void Start()
    {
    }

    public void OnDialogEvent(QuestData data)
    {
        QuestData = data;
        if (data.Dialog)
            NextLineButton();
        else
            EndLine();
    }


    void OnDisable()
    {
        _questData = null;
        canPlay = false;
    }

    public void Play()
    {
        StartCoroutine(PlayLines());
    }

    public void NextLineButton()
    {
        Debug.Log("NextLineButton Pressed");
        float i = 1;
        NextLine(out i);
    }

    public IEnumerator PlayLines()
    {
        while (canPlay)
        {
            float f;
            NextLine(out f);
            yield return new WaitForSeconds(f);
        }
    }

    public void NextLine(out float time)
    {
        line += 1;
        time = 1;
        if (line >= lines.Count)
        {
            EndLine();
            return;
        }

        LineData lineData = lines[line];
        ChooseTalker(lineData.talker);
        textHolder.text = lineData.text;
        time = lineData.time;
    }

    public void EndLine()
    {
        if (_questData == null)
        {
            Exit();
            return;
        }
        if (_questData.status == QuestStatus.Finished && isEnding)
        {
            RemoveQuest();
        }
        else
            AddQuest();
    }

    public void AddQuest()
    {
        if (questSystem.AddQuest(_questData))
        {
            if (_questData)
                Debug.LogWarning($"Dialog System: Quest added {_questData.QuestName}");
            line = -1;
            canPlay = false;
        }
        else
        {
            if (_questData)
                Debug.LogWarning($"Quest not added, already existed: {_questData.QuestName}");
        }
        Exit();
    }

    public void RemoveQuest()
    {
        if (questSystem.RemoveQuest(_questData))
        {
            questSystem.currentFlow.Next();
            //Why is this removing things from inventory
            //The questSystem should do that
            inventorySystem.SpendResources(_questData);
            line = -1;
            canPlay = false;
            if (_questData)
                Debug.LogWarning($"Dialog System: Quest Removed {_questData.QuestName}");
        }
        else
        {
            if (_questData)
                Debug.LogWarning($"Quest not Removed, didn't exist: {_questData.QuestName}");
        }
        Exit();
    }

    public void ChooseTalker(string name)
    {
        Character.color = Darken;
        nameplatePlayer.SetActive(false);
        NPC.color = Darken;
        nameplateNPC.SetActive(false);
        if (name.Contains("NPC"))
        {
            Vector3 v = new Vector3(-1, 1, 1);
            textBackground.localScale = v;
            nameplateNPC.SetActive(true);
            nameplatePlayer.SetActive(false);
            NPC.color = Color.white;
            nameplateNPC.SetActive(true);
        }
        else
        {
            textBackground.localScale = textBackgroundStartScale;
            nameplateNPC.SetActive(false);
            nameplatePlayer.SetActive(true);
            Character.color = Color.white;
            nameplatePlayer.SetActive(true);
        }

    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
