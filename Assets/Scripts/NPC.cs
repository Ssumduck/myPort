using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    public Player player;
    [SerializeField]
    Define.NPCType type = Define.NPCType.NONE;

    public Sprite btnSprite;

    [SerializeField]
    TextMeshPro questMark;

    public string NPCName;
    
    public int index;

    public QuestList questList;
    public ShopUI shopUI;
    public List<Quest> quests = new List<Quest>();
    public List<Item> shopItem = new List<Item>();

    [SerializeField]
    Define.QuestMark mark = Define.QuestMark.NONE;

    private void Awake()
    {
        gameObject.name = $"NPC{index}";
        switch (type)
        {
            case Define.NPCType.TALK:
                break;
            case Define.NPCType.SHOP:
                shopUI = GameObject.Find("UI").transform.Find("ShopUI").GetComponent<ShopUI>();
                break;
            case Define.NPCType.QUEST:
                questList = GameObject.Find("UI").transform.Find("NPCQuestList").GetComponent<QuestList>();
                break;
        }
    }

    public void Talk()
    {
        switch (type)
        {
            case Define.NPCType.TALK:
                Debug.Log("TALK");
                break;
            case Define.NPCType.SHOP:
                shopUI.Init(this);
                break;
            case Define.NPCType.QUEST:
                Quest();
                break;
        }
    }

    private void Update()
    {
        QuestMarkCheck();
        questMark.transform.LookAt(Camera.main.transform);

        switch (mark)
        {
            case Define.QuestMark.NONE:
                questMark.text = "";
                break;
            case Define.QuestMark.HAS:
                questMark.text = "!";
                break;
            case Define.QuestMark.COMPLETE:
                questMark.text = "?";
                break;
        }
    }

    void QuestMarkCheck()
    {
        for (int i = 0; i < player.myQuest.Count; i++)
        {
            if(player.myQuest[i].DestNpcIndex == index)
            {
                mark = Define.QuestMark.COMPLETE;
                return;
            }
        }

        if (quests.Count != 0)
        {
            mark = Define.QuestMark.HAS;
        }
        else
            mark = Define.QuestMark.NONE;
    }

    void Quest()
    {
        List<Quest> quests = new List<Quest>();

        quests.AddRange(GetComponentsInChildren<Quest>());

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].Level > player.myStat.Level || !player.questClear.ContainsKey(quests[i].PrevQuest))
                quests.RemoveAt(i);
        }

        questList.DialogueInit(this, quests);
    }

    public void QuestAccept(Quest _quest)
    {
        Quest[] _quests = transform.GetComponentsInChildren<Quest>();

        for (int i = 0; i < _quests.Length; i++)
        {
            if(_quests[i].Index == _quest.Index && !_quests[i].accept)
            {
                GameObject go = _quests[i].gameObject;
                quests.Remove(_quests[i]);
                Destroy(go);
            }
        }
    }

}
