using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    int index;
    [SerializeField]
    int npcIndex;
    [SerializeField]
    string questName;
    [SerializeField]
    string explain;
    [SerializeField]
    Define.QuestType type = Define.QuestType.NONE;
    [SerializeField]
    int level;
    [SerializeField]
    int prevQuest;
    [SerializeField]
    int destNpcIndex;
    [SerializeField]
    int gold;
    [SerializeField]
    List<Item> items = new List<Item>();
    [SerializeField]
    List<int> itemCount = new List<int>();
    [SerializeField]
    int exp;
    public bool Clear { get; set; } = false;

    static GameObject go;

    public int Index { get { return index; } }
    public int NPCIndex { get { return npcIndex; } }
    public int DestNpcIndex { get { return destNpcIndex; } }
    public string Name { get { return questName; } }
    public string Explain { get { return explain; } }
    public int Level { get { return level; } }
    public int PrevQuest { get { return prevQuest; } }
    public int EXP { get { return exp; } }
    public int Gold { get { return gold; } }
    public List<Item> Items { get { return items; } }
    public List<int> ItemCount { get { return itemCount; } }
    public bool accept = false;

    public Quest(int _index, int _npcIndex, string _questName, string _explain , Define.QuestType _type, int _level
                , int _prevQuest, int _destNpcIndex, int _gold, List<Item> _items, List<int> _itemCount, int _exp )
    {
        index = _index;
        npcIndex = _npcIndex;
        questName = _questName;
        explain = _explain;
        type = _type;
        level = _level;
        prevQuest = _prevQuest;
        destNpcIndex = _destNpcIndex;
        gold = _gold;
        items = _items;
        itemCount = _itemCount;
        exp = _exp;


        NPC npc = NPCUtil.NPCIndexReturn(npcIndex);

        go = new GameObject("@Quest");
        go.transform.SetParent(npc.transform);
    }

    Quest() { }

    public static void QuestInit(Quest _quest)
    {
        Quest quest = go.AddComponent<Quest>();
        quest.index = _quest.index;
        quest.npcIndex = _quest.npcIndex;
        quest.questName = _quest.questName;
        quest.type = _quest.type;
        quest.level = _quest.level;
        quest.prevQuest = _quest.prevQuest;
        quest.destNpcIndex = _quest.destNpcIndex;
        quest.gold = _quest.gold;
        quest.items = _quest.items;
        quest.itemCount = _quest.itemCount;
        quest.exp = _quest.exp;
        quest.explain = _quest.explain;

        NPC npc = NPCUtil.NPCIndexReturn(quest.npcIndex);
        npc.quests.Add(quest);
    }

    public static Quest QuestClone(Quest _quest)
    {
        Player player = GameObject.FindObjectOfType<Player>();
        Quest quest = player.gameObject.AddComponent<Quest>();
        quest.index = _quest.index;
        quest.npcIndex = _quest.npcIndex;
        quest.questName = _quest.questName;
        quest.type = _quest.type;
        quest.level = _quest.level;
        quest.prevQuest = _quest.prevQuest;
        quest.destNpcIndex = _quest.destNpcIndex;
        quest.gold = _quest.gold;
        quest.items = _quest.items;
        quest.itemCount = _quest.itemCount;
        quest.exp = _quest.exp;
        quest.explain = _quest.explain;

        return quest;
    }

    public void QuestClear(Quest _quest)
    {
        Player player = GameObject.FindObjectOfType<Player>();

        player.myStat.currEXP += _quest.EXP;
        player.myStat.Gold += _quest.Gold;

        for (int i = 0; i < _quest.Items.Count; i++)
        {
            for (int j = 0; j < _quest.ItemCount[i]; j++)
            {
                ItemUtil.GetItem(_quest.Items[i]);
            }
        }
        player.myQuest.Remove(_quest);
        player.questClear.Add(_quest.Index, true);
        Destroy(_quest);

        player.LevelUp();
    }

    private void Update()
    {
        switch (type)
        {
            case Define.QuestType.TALK:
                Clear = true;
                break;
            case Define.QuestType.HUNT:
                break;
        }
    }
}
