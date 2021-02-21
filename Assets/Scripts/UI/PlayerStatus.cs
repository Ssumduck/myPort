using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Text text, HPText, MPText, EXPText;
    [SerializeField]
    Slider hpSlider, mpSlider, expSlider;

    void Init()
    {
        text.text = $"LV {player.myStat.Level} : {player.myStat.Name}";

        HPText.text = $"{player.myStat.currHP}/{player.myStat.HP}";
        MPText.text = $"{player.myStat.currMP}/{player.myStat.MP}";
        EXPText.text = $"{player.myStat.currEXP}/{player.myStat.EXP}";

        hpSlider.value = (float)player.myStat.currHP / player.myStat.HP;
        mpSlider.value = (float)player.myStat.currMP / player.myStat.MP;
        expSlider.value = (float)player.myStat.currEXP / player.myStat.EXP;
    }

    private void Update()
    {
        Init();
    }
}