using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    public static Player player;

    public Dictionary<int, Item> ItemData = new Dictionary<int, Item>();
    public Dictionary<int, Skill> SkillData = new Dictionary<int, Skill>();
    public Dictionary<int, Skill> SkillSlotData = new Dictionary<int, Skill>();

    public List<Skill> skills = new List<Skill>();
    public List<Item> equipItem = new List<Item>();
    public List<Quest> quests = new List<Quest>();

    public void Init()
    {
        player = GameObject.FindObjectOfType<Player>();

        ItemInit();
        QuestInit();
        ShopDataInit();
        SkillDataInit();
    }

    void ItemInit()
    {
        EquipItemInit();
        UseItemInit();
    }

    void EquipItemInit()
    {
        TextAsset equipItemData = Resources.Load<TextAsset>("Data/EquipItem");

        string[] row = equipItemData.text.Split('\n');

        for (int i = 1; i < row.Length - 1; i++)
        {
            string[] data = row[i].Split('\t');

            equipItem.Add(new Item(data[0], data[1], (Define.EquipmentType)Enum.Parse(typeof(Define.EquipmentType), data[2]), data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14], data[15], data[16], data[17]));

            ItemData.Add(equipItem[i - 1].Index, equipItem[i - 1]);
        }
    }

    void UseItemInit()
    {
        TextAsset useItemData = Resources.Load<TextAsset>("Data/UseItem");

        string[] row = useItemData.text.Split('\n');

        for (int i = 1; i < row.Length - 1; i++)
        {
            string[] data = row[i].Split('\t');

            Item item = new Item(int.Parse(data[0]), (Define.ItemType)Enum.Parse(typeof(Define.ItemType), data[1]), data[2], data[3], int.Parse(data[4]),
                                int.Parse(data[5]), int.Parse(data[6]), int.Parse(data[7]), int.Parse(data[8]), int.Parse(data[9]), int.Parse(data[10]),
                                int.Parse(data[11]), int.Parse(data[12]), int.Parse(data[13]), int.Parse(data[14]), int.Parse(data[15]), int.Parse(data[16]),
                                int.Parse(data[17]), int.Parse(data[18]), int.Parse(data[19]));

            ItemData.Add(item.Index, item);
        }
    }

    void QuestInit()
    {
        TextAsset questData = Resources.Load<TextAsset>("Data/Quest");

        string[] row = questData.text.Split('\n');

        for (int i = 1; i < row.Length - 1; i++)
        {
            //8
            List<Item> items = new List<Item>();
            //9
            List<int> itemCount = new List<int>();

            string[] data = row[i].Split('\t');

            string[] itemdata = data[9].Split('|');
            string[] countData = data[10].Split('|');

            for (int j = 0; j < itemdata.Length; j++)
            {
                if (String.IsNullOrEmpty(itemdata[j]))
                    continue;
                items.Add(ItemData[int.Parse(itemdata[j])]);
                itemCount.Add(int.Parse(countData[j]));
            }

            Quest quest = new Quest(int.Parse(data[0]), int.Parse(data[1]), data[2], data[3] , (Define.QuestType)Enum.Parse(typeof(Define.QuestType), data[4]),
                int.Parse(data[5]), int.Parse(data[6]), int.Parse(data[7]), int.Parse(data[8]), items, itemCount, int.Parse(data[11]));
            Quest.QuestInit(quest);
            quests.Add(quest);
        }
    }

    void ShopDataInit()
    {
        TextAsset text = Resources.Load<TextAsset>("Data/Shop");

        string[] row = text.text.Split('\n');

        for (int i = 1; i < row.Length - 1; i++)
        {
            string[] data = row[i].Split('\t');

            // data[0] NPCINDEX data[1] ITEMINDEX data[2] PRICE

            NPC npc = NPCUtil.NPCIndexReturn(int.Parse(data[0]));
            Item item = Item.Clone(ItemData[int.Parse(data[1])]);
            item.Type = Define.ItemType.BUY;
            npc.shopItem.Add(item);
        }
    }

    void SkillDataInit()
    {
        TextAsset skillData = Resources.Load<TextAsset>("Data/SkillData");

        string[] row = skillData.text.Split('\n');

        for (int i = 1; i < row.Length - 1; i++)
        {
            string[] data = row[i].Split('\t');

            string[] statsData = data[6].Split(',');
            string[] ratiosData = data[7].Split(',');

            List<Define.Stat> stats = new List<Define.Stat>();
            List<float> ratios = new List<float>();

            for (int j = 0; j < statsData.Length; j++)
            {
                Define.Stat stat = (Define.Stat)Enum.Parse(typeof(Define.Stat), statsData[j]);
                float ratio = float.Parse(ratiosData[j]);

                stats.Add(stat);
                ratios.Add(ratio);
            }


            Skill skill = new Skill(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]), (Define.SkillType)Enum.Parse(typeof(Define.SkillType), data[3]), 
                                    data[4], data[5], stats, ratios, float.Parse(data[8]) );

            SkillData.Add(skill.Index, skill);
            SkillSlotData.Add(skill.SlotIndex, skill);
            player.mySkill.Add(skill, 0);
            skills.Add(skill);
        }
    }

    public Tuple<int, int> EXPData(int level)
    {
        TextAsset leveldata = Resources.Load<TextAsset>("Data/Level");

        string[] row = leveldata.text.Split('\n');

        for (int i = 1; i < row.Length - 1; i++)
        {
            string[] data = row[i].Split('\t');

            if (level == int.Parse(data[0]))
            {
                Tuple<int, int> tuple = new Tuple<int, int>(int.Parse(data[1]), int.Parse(data[2]));

                return tuple;
            }
        }
        return null;
    }
}
