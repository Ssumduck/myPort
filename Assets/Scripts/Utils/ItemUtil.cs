using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ItemUtil
{
    static Inventory inven;
    static Player player;

    public static void Init()
    {
        inven = GameObject.Find("UI").transform.Find("Inventory").GetComponent<Inventory>();
        player = GameObject.FindObjectOfType<Player>();
    }

    public static void GetItem(Item item)
    {
        if (inven == null)
            Init();
        Item _item = Item.Clone(item);
        if (_item.Type != Define.ItemType.Equipment)
        {
            for (int i = 0; i < inven.items.Count; i++)
            {
                if (inven.items[i].Index == _item.Index)
                {
                    if (inven.items[i].MaxCount > inven.items[i].ItemCount)
                    {
                        inven.items[i].ItemCount++;
                        return;
                    }
                }
            }
        }

        InventoryIndexInit(_item);
        inven.items.Add(_item);
    }
    public static void InventoryIndexChange(int item1, int item2)
    {
        if (inven == null)
            Init();
        Item _item1 = ReturnItem(item1);
        Item _item2 = ReturnItem(item2);

        if (_item2 == null)
        {
            _item1.inventoryIndex = item2;
            return;
        }
        else if (_item1 == null)
        {
            _item2.inventoryIndex = item1;
            return;
        }

        int temp = _item1.inventoryIndex;
        _item1.inventoryIndex = _item2.inventoryIndex;
        _item2.inventoryIndex = temp;
    }

    /// <summary>
    /// Index를 받아 인벤토리에 index번째에 있는 아이템을 리턴합니다.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static Item ReturnItem(int index)
    {
        for (int i = 0; i < inven.items.Count; i++)
        {
            if (inven.items[i].inventoryIndex == index)
                return inven.items[i];
        }
        return null;
    }
    
    static void InventoryIndexInit(Item item)
    {
        item.InventoryIndex = InventoryIndexSort();
    }

    static int InventoryIndexSort()
    {
        int index = 0;

        List<int> indexList = new List<int>();
        for (int i = 0; i < inven.items.Count; i++)
        {
            indexList.Add(inven.items[i].InventoryIndex);
        }
        indexList.Sort();
        for (int i = 0; i < indexList.Count; i++)
        {
            if (indexList[i] == i)
            {
                index = i + 1;
                continue;
            }
            return i;
        }

        return index;
    }

    //public static void ItemInfo(Item item, GameObject popup, Transform slot)
    //{
    //    popup.SetActive(true);
    //    popup.transform.position = new Vector2(slot.position.x + 300, slot.position.y - 165);

    //    Text nameTxt = popup.transform.GetChild(0).GetComponent<Text>();
    //    Text typeTxt = popup.transform.GetChild(1).GetComponent<Text>();
    //    Text explainTxt = popup.transform.GetChild(2).GetComponent<Text>();
    //    Button btn = popup.transform.GetChild(3).GetComponent<Button>();

    //    nameTxt.text = item.Name;
    //    explainTxt.text = item.Explain + $"({item.Price})";

    //    InventorySlot invenSlot = slot.GetComponent<InventorySlot>();

    //    switch (item.Type)
    //    {
    //        case Define.ItemType.Equipment:
    //            typeTxt.text = "장비 아이템";
    //            btn.onClick.RemoveAllListeners();
    //            btn.onClick.AddListener(() => UseItem(item, invenSlot, popup));
    //            break;
    //        case Define.ItemType.Use:
    //            typeTxt.text = "소비 아이템";
    //            btn.onClick.RemoveAllListeners();
    //            btn.onClick.AddListener(() => UseItem(item, invenSlot, popup));
    //            break;
    //        case Define.ItemType.ETC:
    //            typeTxt.text = "기타 아이템";
    //            btn.onClick.RemoveAllListeners();
    //            btn.onClick.AddListener(() => UseItem(item, invenSlot, popup));
    //            break;
    //        case Define.ItemType.Quest:
    //            typeTxt.text = "퀘스트 아이템";
    //            btn.onClick.RemoveAllListeners();
    //            btn.onClick.AddListener(() => UseItem(item, invenSlot, popup));
    //            break;
    //        case Define.ItemType.BUY:
    //            btn = popup.transform.GetChild(3).GetComponent<Button>();
    //            btn.onClick.RemoveAllListeners();
    //            btn.onClick.AddListener(() => BuyItem(item));
    //            break;
    //    }
    //}

    public static void BuyItem(Item _item)
    {
        if (player == null)
            Init();
        Item item = Item.Clone(Managers.Data.ItemData[_item.Index]);
        if (player.myStat.Gold >= item.Price)
        {
            player.myStat.Gold -= item.Price;
            ItemUtil.GetItem(item);
        }
    }

    public static void ItemUse(Item item, InventorySlot slot)
    {
        switch (item.Type)
        {
            case Define.ItemType.Equipment:
                EquipmentItem(item, slot);
                break;
            case Define.ItemType.Use:
                UseItem(item, slot);
                break;
            case Define.ItemType.ETC:
                break;
            case Define.ItemType.Quest:
                break;
        }
    }

    static void UseItem(Item item, InventorySlot slot)
    {
        if (player.myStat.currHP + item.currHP > player.myStat.HP)
            player.myStat.currHP = player.myStat.HP;
        else
            player.myStat.currHP += item.currHP;

        if (player.myStat.currMP + item.currMP > player.myStat.MP)
            player.myStat.currMP = player.myStat.MP;
        else
            player.myStat.currMP += item.currMP;

        if (item.ItemCount == 1)
            inven.items.Remove(item);
        else
            item.ItemCount--;

        slot.SlotInit(ReturnItem(slot.transform.GetSiblingIndex()));
    }

    static void EquipmentItem(Item item, InventorySlot slot)
    {
        if (player.Equipment.ContainsKey(item.EquipType))
        { Debug.Log("이미 장착된 슬롯입니다."); return; }

        player.Equipment.Add(item.EquipType, item);
        inven.items.Remove(item);

        slot.SlotInit(ReturnItem(slot.transform.GetSiblingIndex()));

        player.myStat.HP += item.HP;
        player.myStat.HPRecovery += item.HPRecovery;
        player.myStat.MP += item.MP;
        player.myStat.MPRecovery += item.MPRecovery;
        player.myStat.STR += item.STR;
        player.myStat.DEX += item.DEX;
        player.myStat.INT += item.INT;
        player.myStat.LUK += item.LUK;
        player.myStat.AD += item.AD;
        player.myStat.AP += item.AP;
        player.myStat.DEF += item.DEF;
        player.myStat.MR += item.MR;

        Debug.Log("장착완료");
    }

    public static void UnEquipmentItem(Item item)
    {
        player.myStat.HP -= item.HP;
        player.myStat.HPRecovery -= item.HPRecovery;
        player.myStat.MP -= item.MP;
        player.myStat.MPRecovery -= item.MPRecovery;
        player.myStat.STR -= item.STR;
        player.myStat.DEX -= item.DEX;
        player.myStat.INT -= item.INT;
        player.myStat.LUK -= item.LUK;
        player.myStat.AD -= item.AD;
        player.myStat.AP -= item.AP;
        player.myStat.DEF -= item.DEF;
        player.myStat.MR -= item.MR;
    }
}
