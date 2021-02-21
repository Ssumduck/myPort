using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public InventorySlot invenSlot;
    public Define.TooltipType type = Define.TooltipType.NONE;

    public Skill skill;

    public Item item;

    public Text nameTxt;
    public Text typeTxt;
    public Text explainTxt;

    public Text applyTxt;

    public Button btn0, btn1;

    List<string> data = new List<string>();

    public void Init()
    {
        data.Clear();

        switch (type)
        {
            case Define.TooltipType.ITEM:
                data.Add(item.Name);
                data.Add(item.Explain + $"({item.Price})");
                data.Add(Managers.Data.ItemData[item.Index].Type.ToString());

                switch (item.Type)
                {
                    case Define.ItemType.Equipment:
                        data.Add("장비 아이템");
                        applyTxt.text = "장착";
                        btn0.onClick.RemoveAllListeners();
                        btn0.onClick.AddListener(() => ItemUtil.ItemUse(item, invenSlot));
                        break;
                    case Define.ItemType.Use:
                        data.Add("소비 아이템");
                        applyTxt.text = "사용";
                        btn0.onClick.RemoveAllListeners();
                        btn0.onClick.AddListener(() => ItemUtil.ItemUse(item, invenSlot));
                        break;
                    case Define.ItemType.ETC:
                        data.Add("기타 아이템");
                        applyTxt.text = string.Empty;
                        btn0.onClick.RemoveAllListeners();
                        btn0.onClick.AddListener(() => ItemUtil.ItemUse(item, invenSlot));
                        break;
                    case Define.ItemType.Quest:
                        data.Add("퀘스트 아이템");
                        applyTxt.text = string.Empty;
                        btn0.onClick.RemoveAllListeners();
                        btn0.onClick.AddListener(() => ItemUtil.ItemUse(item, invenSlot));
                        break;
                    case Define.ItemType.BUY:
                        applyTxt.text = "구입";
                        btn0.onClick.RemoveAllListeners();
                        btn0.onClick.AddListener(() => ItemUtil.BuyItem(item));
                        break;
                }

                break;
            case Define.TooltipType.SKILL:
                data.Add(skill.Name);
                data.Add(skill.Explain);
                data.Add(skill.Type);
                btn0.onClick.AddListener(() => SkillUtil.SkillLevelUp(skill));
                break;
        }

        nameTxt.text = data[0];
        explainTxt.text = data[1];
        typeTxt.text = data[2];

        btn1.onClick.AddListener(() => Destroy(gameObject));
        btn0.onClick.AddListener(() => Destroy(gameObject));
    }
}
