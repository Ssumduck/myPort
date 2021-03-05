using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    Transform tf;

    int slotIndex;
    int index;
    int prevIndex;
    public Define.SkillType type = Define.SkillType.NONE;
    string name;
    string explain;
    List<Define.Stat> stats = new List<Define.Stat>();
    List<float> ratios = new List<float>();
    float time;
    bool buff = false;
    float effect_x, effect_y, effect_z;

    public int Index { get { return index; } }
    public int PrevIndex { get { return prevIndex; } }
    public int SlotIndex { get { return slotIndex; } set { slotIndex = value; } }
    public string Name { get { return name; } }
    public string Explain { get { return explain; } }
    public string Type
    {
        get
        {
            switch (type)
            {
                case Define.SkillType.Buff:
                    return "버프";
                case Define.SkillType.Active:
                    return "액티브";
                case Define.SkillType.Passive:
                    return "패시브";
                case Define.SkillType.Channeling:
                    return "채널링";
            }
            return type.ToString();
        }
    }
    public bool Buff { get { return buff; } set { buff = value; } }

    public Vector3 EffectPosition
    {
        get { return new Vector3(effect_x, effect_y, effect_z); }
    }


    public Skill(int _slotIndex, int _index, int _prevIndex, Define.SkillType _type, string _name, string _explain, List<Define.Stat> _stats, List<float> _ratios, float _time
                 , string parentName, float x, float y, float z)
    {
        slotIndex = _slotIndex;
        index = _index;
        prevIndex = _prevIndex;
        type = _type;
        name = _name;
        explain = _explain;
        stats = _stats;
        ratios = _ratios;
        time = _time;

        tf = GameDataManager.player.transform.Find(parentName);
        if (tf == null)
            tf = GameDataManager.player.transform;

        effect_x = x;
        effect_y = y;
        effect_z = z;
    }

    public static void UseSkill(Skill skill)
    {
        switch (skill.type)
        {
            case Define.SkillType.Buff:
                BuffSkill(skill);
                break;
            case Define.SkillType.Active:
                break;
            case Define.SkillType.Passive:
                break;
            case Define.SkillType.Channeling:
                break;
            case Define.SkillType.Enchant:
                Enchant(skill);
                break;
        }
    }

    public static IEnumerator Enchant(Skill skill)
    {
        if (skill.buff)
            yield return null;

        skill.buff = true;

        GameDataManager.player.dotDmg = (int)skill.ratios[0];
        GameDataManager.player.dotTime = skill.time;

        switch (skill.stats[0])
        {
            case Define.Stat.FIRE:
                GameDataManager.player.dotType = Define.DOTType.BURN;
                break;
        }


        GameDataManager.player.SkillEffect(skill);

        GameObject go = Managers.Instantiate(Resources.Load($"SkillParticle/{skill.Index}") as GameObject, skill.tf);
        go.transform.localPosition = skill.EffectPosition;

        yield return new WaitForSeconds(skill.time);
        DeEnchant(skill);

        go.GetComponent<ParticleSystem>().Stop();

        Managers.Destroy(go, 10f);
    }
    public static void DeEnchant(Skill skill)
    {
        skill.buff = false;

        GameDataManager.player.dotDmg = 0;
        GameDataManager.player.dotTime = 0;
        GameDataManager.player.dotType = Define.DOTType.NONE;
    }

    public static IEnumerator BuffSkill(Skill skill)
    {
        skill.Buff = true;
        for (int i = 0; i < skill.stats.Count; i++)
        {
            float ratio = skill.ratios[i];

            switch (skill.stats[i])
            {
                case Define.Stat.STR:
                    GameDataManager.player.myStat.STR += (int)ratio;
                    break;
                case Define.Stat.DEX:
                    GameDataManager.player.myStat.DEX += (int)ratio;
                    break;
                case Define.Stat.INT:
                    GameDataManager.player.myStat.INT += (int)ratio;
                    break;
                case Define.Stat.LUK:
                    GameDataManager.player.myStat.LUK += (int)ratio;
                    break;
                case Define.Stat.SPEED:
                    GameDataManager.player.myStat.moveSpeed *= (ratio / 100);
                    break;
                case Define.Stat.ATKSpeed:
                    GameDataManager.player.myStat.atkTime *= (ratio / 100);
                    break;
                case Define.Stat.AD:
                    GameDataManager.player.myStat.AD += (int)ratio;
                    break;
                case Define.Stat.AP:
                    GameDataManager.player.myStat.AP += (int)ratio;
                    break;
                case Define.Stat.DEF:
                    GameDataManager.player.myStat.DEF += (int)ratio;
                    break;
                case Define.Stat.MR:
                    GameDataManager.player.myStat.MR += (int)ratio;
                    break;
            }
        }

        GameDataManager.player.SkillEffect(skill);

        GameObject go = Managers.Instantiate(Resources.Load($"SkillParticle/{skill.Index}") as GameObject, skill.tf);
        go.transform.localPosition = skill.EffectPosition;

        yield return new WaitForSeconds(skill.time);
        CancleBuffSkill(skill);

        go.GetComponent<ParticleSystem>().Stop();

        Managers.Destroy(go, 10f);
    }

    public static void CancleBuffSkill(Skill skill)
    {
        for (int i = 0; i < skill.stats.Count; i++)
        {
            float ratio = skill.ratios[i];

            switch (skill.stats[i])
            {
                case Define.Stat.STR:
                    GameDataManager.player.myStat.STR -= (int)ratio;
                    break;
                case Define.Stat.DEX:
                    GameDataManager.player.myStat.DEX -= (int)ratio;
                    break;
                case Define.Stat.INT:
                    GameDataManager.player.myStat.INT -= (int)ratio;
                    break;
                case Define.Stat.LUK:
                    GameDataManager.player.myStat.LUK -= (int)ratio;
                    break;
                case Define.Stat.SPEED:
                    GameDataManager.player.myStat.moveSpeed /= (ratio / 100);
                    break;
                case Define.Stat.ATKSpeed:
                    GameDataManager.player.myStat.atkTime /= (ratio / 100);
                    break;
                case Define.Stat.AD:
                    GameDataManager.player.myStat.AD -= (int)ratio;
                    break;
                case Define.Stat.AP:
                    GameDataManager.player.myStat.AP -= (int)ratio;
                    break;
                case Define.Stat.DEF:
                    GameDataManager.player.myStat.DEF -= (int)ratio;
                    break;
                case Define.Stat.MR:
                    GameDataManager.player.myStat.MR -= (int)ratio;
                    break;
            }
        }
        skill.Buff = false;

    }
}
