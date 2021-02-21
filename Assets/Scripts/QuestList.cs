using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    Button[] content;
    [SerializeField]
    Text[] questNames;
    [SerializeField]
    QuestDialogue dialogue;
    [SerializeField]
    GameObject[] questlist;
    int j;


    public void DialogueInit(NPC npc, List<Quest> quests)
    {
        CloseDialogue();

        gameObject.SetActive(true);

        nameText.text = npc.NPCName;

        j = 0;

        WorkQuest(npc, quests);
        CompleteQuest(npc, quests);

        for (int i = j; i < quests.Count; i++)
        {
            int k = i;

            if (!questlist[0].activeSelf)
            {
                questlist[0].SetActive(true);
                questlist[0].transform.SetSiblingIndex(content[i].transform.GetSiblingIndex() - 1);
            }

            content[i].gameObject.SetActive(true);

            questNames[i].text = $"{quests[i].Name}";

            content[j].onClick.AddListener(() => dialogue.Init(quests[k], Define.QuestState.NONE));
        }
    }



    void WorkQuest(NPC npc, List<Quest> quests)
    {
        for (int i = 0; i < npc.player.myQuest.Count; i++)
        {
            int k = j;
            if (npc.player.myQuest[i].Clear)
                continue;
            if (!questlist[1].activeSelf)
            {
                questlist[1].SetActive(true);
                questlist[1].transform.SetSiblingIndex(j + 2);
            }

            if (npc.index == npc.player.myQuest[i].DestNpcIndex && !npc.player.myQuest[i].Clear)
            {
                content[i].gameObject.SetActive(true);
                questNames[i].text = $"{quests[i].Name}";
                content[k].onClick.AddListener(() => dialogue.Init(quests[k], Define.QuestState.Accept));
                j++;
            }
        }
    }

    void CompleteQuest(NPC npc, List<Quest> quests)
    {
        for (int i = 0; i < npc.player.myQuest.Count; i++)
        {
            int k = j;
            if (!npc.player.myQuest[i].Clear)
                continue;
            questlist[2].SetActive(true);

            if (npc.index == npc.player.myQuest[i].DestNpcIndex && npc.player.myQuest[i].Clear)
            {
                content[i].gameObject.SetActive(true);
                questNames[i].text = $"{npc.player.myQuest[i].Name}";
                content[k].onClick.AddListener(() => dialogue.Init(npc.player.myQuest[k], Define.QuestState.COMPLETE));
                j++;
            }
        }
    }

    private void OnDisable()
    {
        CloseDialogue();
    }

    void CloseDialogue()
    {
        for (int i = 0; i < content.Length; i++)
        {
            content[i].gameObject.SetActive(false);
            content[i].onClick.RemoveAllListeners();
        }

        for (int i = 0; i < questlist.Length; i++)
        {
            questlist[i].SetActive(false);
        }
    }
}
