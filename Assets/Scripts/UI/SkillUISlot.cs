using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillUISlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    GameObject popup;
    [SerializeField]
    Color color;

    Skill skill;
    public int index;
    Image Skillimg;

    public Skill prevSkill;

    GameObject slot;

    GraphicRaycaster gr;

    private void Awake()
    {
        Skillimg = transform.GetChild(0).GetComponent<Image>();
        gr = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
    }

    private void OnEnable()
    {
        SlotImgInit();
    }

    public void SlotImgInit()
    {
        RectTransform rect = Skillimg.GetComponent<RectTransform>();
        skill = SkillUtil.SkillSlotIndex(this);

        if (skill == null) return;

        if (skill.PrevIndex != 0)
        {
            prevSkill = Managers.Data.SkillData[skill.PrevIndex];

            SkillUtil.SkillTreeDrawLine(this);
        }

        if (skill == null)
        {
            rect.sizeDelta = new Vector2(100, 100);
            return;
        }

        Skillimg.sprite = Resources.Load<Sprite>($"Image/SkillIcon/{skill.Index}");
        rect.sizeDelta = new Vector2(78, 75);

        if (GameDataManager.player.mySkill[skill] == 0)
            Skillimg.GetComponent<Image>().color = color;
        else
            Skillimg.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Tooltip.ToolTipCreate(transform, Define.TooltipType.SKILL, null, skill);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameDataManager.player.mySkill[skill] == 0)
            return;
        slot = Instantiate(gameObject, transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(slot != null)
            slot.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slot != null)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(eventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.GetComponent<SkillSlot>() == null)
                    continue;

                SkillSlot skillSlot = results[i].gameObject.GetComponent<SkillSlot>();
                skillSlot.skill = skill;
                skillSlot.SlotImgInit();
                break;
            }

            Destroy(slot);
        }
    }
}
