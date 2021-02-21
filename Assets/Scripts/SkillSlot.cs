using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{

    public Sprite defaultSpr;
    
    public Skill skill;

    Image slotimg;

    private void Awake()
    {
        SlotImgInit();
    }

    public void SlotImgInit()
    {
        if (slotimg == null)
            slotimg = transform.GetChild(0).GetComponent<Image>();

        if (skill == null) { slotimg.sprite = defaultSpr; slotimg.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100); }
        else { slotimg.sprite = Resources.Load<Sprite>($"Image/SkillIcon/{skill.Index}"); slotimg.GetComponent<RectTransform>().sizeDelta = new Vector2(78, 75); }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skill == null)
            return;

        if(!skill.Buff)
            StartCoroutine(Skill.BuffSkill(skill));
    }
}