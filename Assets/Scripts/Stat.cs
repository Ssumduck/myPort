using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat")]
public class Stat : ScriptableObject
{
    public Sprite spr;
    public string myName;
    public int index;
    public int lv;
    public int gold;
    public int currhp;
    public int hp;
    public int hpRecovery;
    public int mp;
    public int currmp;
    public int mpRecovery;
    public int str;
    public int dex;
    public int Int;
    public int luk;
    public int ad;
    public int ap;
    public int def;
    public int mr;
    public int exp;
    public int currexp;
    public int statPoint;
    public int skillPoint;
    public float atktime;
    public float atkDistance;
    public float movespeed;
    public float moveTime;
    public float dieanimationTime;
    public float dieElapsed;
    public float spawnTime;
    public float moveDuration;
}
[System.Serializable]
public class MyStat
{
    public Sprite spr;
    public string Name;
    public int index;
    public int Level;
    public int Gold;
    public int currHP;
    public int HP;
    public int HPRecovery;
    public int MP;
    public int currMP;
    public int MPRecovery;
    public int STR;
    public int DEX;
    public int INT;
    public int LUK;
    public int AD;
    public int AP;
    public int DEF;
    public int MR;
    public int EXP;
    public int currEXP;
    public int statPoint;
    public int skillPoint;
    public float atkTime;
    public float atkDistance;
    public float moveSpeed;
    public float moveTime;
    public float dieanimationTime;
    public float dieElapsed;
    public float moveElapsed = 0f;
    public float moveDuration;

    public MyStat(Stat stat)
    {
        Name = stat.myName;
        Level = stat.lv;
        Gold = stat.gold;
        HP = stat.hp;
        currHP = stat.hp;
        HPRecovery = stat.hpRecovery;
        MP = stat.mp;
        currMP = stat.mp;
        MPRecovery = stat.mpRecovery;
        STR = stat.str;
        DEX = stat.dex;
        INT = stat.Int;
        LUK = stat.luk;
        AD = stat.ad;
        AP = stat.ap;
        DEF = stat.def;
        MR = stat.mr;
        EXP = stat.exp;
        currEXP = stat.currexp;
        atkTime = stat.atktime;
        moveSpeed = stat.movespeed;
        moveTime = stat.moveTime;
        statPoint = stat.statPoint;
        skillPoint = stat.skillPoint;
        atkDistance = stat.atkDistance;
        dieanimationTime = stat.dieanimationTime;
        moveDuration = stat.moveDuration;
        index = stat.index;
        spr = stat.spr;
    }
}