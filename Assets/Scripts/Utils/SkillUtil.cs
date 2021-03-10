using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUtil
{
    public static int SkillSlotIndex(Skill skill)
    {
        return skill.SlotIndex;
    }

    public static void SkillTreeDrawLine(SkillUISlot slot)
    {
        if (slot.prevSkill == null) return;

        Transform parent = GameObject.Find("UI").transform;
        parent = parent.transform.Find("SkillUI");

        GameObject go = new GameObject("Line");
        go.transform.SetParent(parent);
        go.transform.SetAsFirstSibling();

        UnityEngine.UI.Image img = go.AddComponent<UnityEngine.UI.Image>();
        RectTransform rect = go.GetComponent<RectTransform>();

        SkillUISlot prevSlot = SkillUISlotReturn(slot.prevSkill);

        RectTransform slot1rect = slot.GetComponent<RectTransform>();
        RectTransform slot2rect = prevSlot.GetComponent<RectTransform>();

        float x = (slot1rect.localPosition.x + slot2rect.localPosition.x) / 2;
        float y = (slot1rect.localPosition.y + slot2rect.localPosition.y) / 2;

        rect.localPosition = new Vector2(x, y);

        Vector2 vec = slot2rect.position - slot1rect.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        rect.rotation = Quaternion.Euler(0, 0, angle);

        float distance = Vector2.Distance(slot2rect.position, slot1rect.position);

        rect.sizeDelta = new Vector2(distance, 10);
    }

    public static SkillUISlot SkillUISlotReturn(Skill skill)
    {
        SkillUISlot[] slots = GameObject.FindObjectsOfType<SkillUISlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].index == skill.SlotIndex)
                return slots[i];
        }
        return null;
    }

    public static Skill SkillSlotIndex(SkillUISlot slot)
    {
        if (Managers.Data.SkillSlotData.ContainsKey(slot.index))
            return Managers.Data.SkillSlotData[slot.index];
        else
            return null;
    }

    public static Skill PrevSkillReturn(Skill skill)
    {
        if(Managers.Data.SkillData.ContainsKey(skill.PrevIndex))
            return Managers.Data.SkillData[skill.PrevIndex];
        return null;
    }

    public static void SkillLevelUp(Skill skill)
    {
        if (GameDataManager.player.myStat.skillPoint <= 0)
        {
            Managers.Tooltip.SystemToolTip("스킬 오류", "스킬 포인트가 부족합니다.");
            return;
        }

        if (PrevSkillReturn(skill) != null && GameDataManager.player.mySkill[PrevSkillReturn(skill)] == 0)
        {
            Managers.Tooltip.SystemToolTip("스킬 오류", "선행 스킬을 배워주세요.");
            return;
        }

        if (GameDataManager.player.mySkill[skill] != 0)
        {
            Managers.Tooltip.SystemToolTip("스킬 오류", "해당 스킬은 이미 배웠습니다.");
        }

        int level = GameDataManager.player.mySkill[skill] + 1;

        GameDataManager.player.myStat.skillPoint -= 1;
        GameDataManager.player.mySkill[skill] = level;
        SkillUISlot slot = SkillUISlotReturn(skill);
        slot.SlotImgInit();
        GameObject.FindObjectOfType<SkillUI>().Init();
    }

    public static void UseSkill(Skill skill)
    {
        Debug.Log(skill.Name);
    }
}
