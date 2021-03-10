﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    Text skillPointText;

    public void Init()
    {
        skillPointText.text = $"스킬 포인트 : {GameDataManager.player.myStat.skillPoint.ToString()}";
    }

    private void OnEnable()
    {
        Init();
    }
}
