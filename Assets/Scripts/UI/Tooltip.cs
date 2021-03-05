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
                applyTxt.text = "배우기";
                btn0.onClick.AddListener(() => SkillUtil.SkillLevelUp(skill));
                break;
            case Define.TooltipType.DUNGEON:
                data.Add("보스 입장");
                data.Add("보스방에 입장하시겠습니까?");
                data.Add("");
                applyTxt.text = "입장";
                btn0.onClick.AddListener(() => GameObject.FindObjectOfType<BossContent>().BossRoom());
                transform.localPosition = Vector3.zero;
                break;
            case Define.TooltipType.DIE:
                data.Add("캐릭터가 사망하였습니다.");
                data.Add("부활 하시겠습니까?");
                data.Add("");
                applyTxt.text = "부활";
                btn0.onClick.AddListener(() => GameDataManager.player.Revive());
                break;
        }

        nameTxt.text = data[0];
        explainTxt.text = data[1];
        explainTxt.alignment = TextAnchor.MiddleCenter;
        typeTxt.text = data[2];

        btn1.onClick.AddListener(() => Destroy(gameObject));
        btn0.onClick.AddListener(() => Destroy(gameObject));
    }
}
