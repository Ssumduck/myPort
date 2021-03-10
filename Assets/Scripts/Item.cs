using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // 캐릭터 고유 번호 [0]
    int ID;
    // 아이템 고유 번호 [1]
    int index;
    // 아이템 타입 [2]
    Define.ItemType type = Define.ItemType.NONE;

    // 아이템 이름 [3]
    string name;

    // 아이템 설명 [4]
    string explain;

    Define.EquipmentType equipType = Define.EquipmentType.NONE;

    int itemCount = 1;

    int maxCount = 1;

    // 살때 가격
    int price;

    public int currHP, currMP;
    public float time;

    public int HP, HPRecovery, MP, MPRecovery, STR, DEX, INT, LUK, AD, AP, DEF, MR, ItemLV;

    // 몇번째 슬롯에 있는지
    public int inventoryIndex = 0;

    public int ItemID { get { return ID; } }

    public int InventoryIndex { get { return inventoryIndex; } set { inventoryIndex = value; } }
    public int Index { get { return index; } set { index = value; } }
    public string Name { get { return name; } }
    public string Explain { get { return explain; } }
    public int ItemCount { get { return itemCount; } set { itemCount = value; } }
    public int MaxCount { get { return maxCount; } }
    public int Price { get { return price; } }

    public Define.ItemType Type { get { return type; } set { type = value; } }
    public Define.EquipmentType EquipType { get { return equipType; } }

    private Item()
    {
    }

    /// <summary>
    /// ETC, Quest 아이템 생성자
    /// [1] INDEX [3] ITEMTYPE [5] NAME [6] EXPLAIN [7] PRICE
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_index"></param>
    /// <param name="_type"></param>
    /// <param name="_name"></param>
    /// <param name="_explain"></param>
    /// <param name="_count"></param>
    /// <param name="_maxCount"></param>
    public Item(string _index, Define.ItemType _type, string _name, string _explain, string _price, int _count = 1, int _maxCount = 1)
    {
        index = int.Parse(_index);
        type = _type;
        name = _name;
        explain = _explain;
        inventoryIndex = 0;
        price = int.Parse(_price);
        itemCount = _count;
        maxCount = _maxCount;
    }

    /// <summary>
    /// 장비 아이템 생성자
    /// [0] ID [1] INDEX [2] ItemLV [4] EquipType [5] Name
    /// [6] Explain [7] Price [8] HP [9] HPRecovery [10] MP
    /// [11] MPRecovery [12] STR [13] DEX [14] INT [15] LUK
    /// [16] AD [17] AP [18] DEF [19] MR 
    /// </summary>
    public Item(string _index, string _itemLV, Define.EquipmentType _type, string _name, string _explain, string _price, string _HP, string _HPRecovery,
                string _MP, string _MPRecovery, string _STR, string _DEX, string _INT, string _LUK, string _AD, string _AP, string _DEF, string _MR)
    {
        type = Define.ItemType.Equipment;

        index = int.Parse(_index);
        ItemLV = int.Parse(_itemLV);
        equipType = _type;
        name = _name;
        explain = _explain;
        price = int.Parse(_price);
        HP = int.Parse(_HP);
        HPRecovery = int.Parse(_HPRecovery);
        MP = int.Parse(_MP);
        MPRecovery = int.Parse(_MPRecovery);
        STR = int.Parse(_STR);
        DEX = int.Parse(_DEX);
        INT = int.Parse(_INT);
        LUK = int.Parse(_LUK);
        AD = int.Parse(_AD);
        AP = int.Parse(_AP);
        DEF = int.Parse(_DEF);
        MR = int.Parse(_MR);
        maxCount = 1;
    }

    /// <summary>
    /// 소비용 아이템 생성자
    /// </summary>
    public Item(int _index, Define.ItemType _type, string _name, string _explain, int _price, int _maxCount, int _currHP, int _HP, int _HPRecovery,
                int _currMP, int _MP, int _MPRecovery, int _STR, int _DEX, int _INT, int _LUK, int _AD, int _AP, int _DEF, int _MR)
    {
        index = _index;
        type = _type;
        name = _name;
        explain = _explain;
        price = _price;
        maxCount = _maxCount;
        currHP = _currHP;
        HP = _HP;
        HPRecovery = _HPRecovery;
        currMP = _currMP;
        MP = _MP;
        MPRecovery = _MPRecovery;
        STR = _STR;
        DEX = _DEX;
        INT = _INT;
        LUK = _LUK;
        AD = _AD;
        AP = _AP;
        DEF = _DEF;
        MR = _MR;
    }

    public static Item Clone(Item item)
    {
        Item _item = new Item();

        _item.index = item.index;
        _item.ItemLV = item.ItemLV;
        _item.type = item.type;
        _item.equipType = item.equipType;
        _item.name = item.name;
        _item.explain = item.explain;
        _item.price = item.price;
        _item.currHP = item.currHP;
        _item.currMP = item.currMP;
        _item.HP = item.HP;
        _item.HPRecovery = item.HPRecovery;
        _item.MP = item.MP;
        _item.MPRecovery = item.MP;
        _item.STR = item.STR;
        _item.DEX = item.DEX;
        _item.INT = item.INT;
        _item.LUK = item.LUK;
        _item.AD = item.AD;
        _item.AP = item.AP;
        _item.DEF = item.DEF;
        _item.MR = item.MR;
        _item.maxCount = item.maxCount;

        return _item;
    }
}