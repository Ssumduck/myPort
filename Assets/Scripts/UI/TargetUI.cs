using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    bool check = false;
    Image targetIcon;
    Text nameTxt;
    Slider hpSlider;
    Monster monster;

    void Init()
    {
        targetIcon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        nameTxt = transform.GetChild(1).GetComponent<Text>();
        hpSlider = transform.GetChild(2).GetComponent<Slider>();
        monster = GameDataManager.player.attackTarget;

        check = true;
    }

    private void OnEnable()
    {
        if (!check)
            Init();
    }

    private void Update()
    {
        targetIcon.sprite = monster.myStat.spr;
        nameTxt.text = $"LV.{monster.myStat.Level} {monster.myStat.Name}";
        hpSlider.value = (float)monster.myStat.currHP / (float)monster.myStat.HP;
    }
}
