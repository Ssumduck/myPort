﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Define
{
    public enum MonsterType
    {
        None,
        Warlock,
    }
    public enum PlayerState
    {
        Idle,
        Moving,
        Attack,
        Die,
    }

    public enum MonsterState
    {
        Idle,
        Moving,
        Patrol,
        Trace,
        Attack,
        Die,
    }

    public enum ItemType
    {
        NONE,
        Equipment,
        Use,
        ETC,
        Quest,
        Buff,
        BUY,
    }

    public enum EquipmentType
    {
        NONE,
        Helmat,
        Weapon,
        Armor,
        Glove,
        Ring,
        Shose,
        Shield,
    }

    [Flags]
    public enum NPCType
    {
        NONE = 1,
        TALK = 2,
        SHOP = 4,
        QUEST = 8,
    }

    public enum QuestType
    {
        NONE,
        TALK,
        HUNT,
    }

    public enum QuestMark
    {
        NONE,
        HAS,
        COMPLETE
    }

    public enum QuestState
    {
        NONE,
        Accept,
        COMPLETE,
    }

    public enum SkillType
    {
        NONE,
        Buff,
        Active,
        Passive,
        Channeling,
    }

    public enum Stat
    {
        NONE,
        STR,
        DEX,
        INT,
        LUK,
        SPEED,
        ATKSpeed,
        AD,
        AP,
        DEF,
        MR,
    }

    public enum DamageType
    {
        AD,
        AP,
    }

    public enum TooltipType
    {
        NONE,
        ITEM,
        SKILL
    }
}