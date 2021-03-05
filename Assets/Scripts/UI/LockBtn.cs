using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockBtn : MonoBehaviour
{
    // [0] [1] LockOff 
    // [2] [3] LockOn
    [SerializeField]
    Sprite[] spr = new Sprite[4];

    // [0] BG
    // [1] ICON
    [SerializeField]
    Image[] img = new Image[2];

    bool Lock = false;

    public void LockBtnFunc()
    {
        if (GameDataManager.player.attackTarget == null)
            return;

        Lock = !Lock;

        if (!Lock)
        {
            img[0].sprite = spr[0];
            img[1].sprite = spr[1];
        }
        else
        {
            img[0].sprite = spr[2];
            img[1].sprite = spr[3];
        }

        GameDataManager.player.LockOn = Lock;
    }
}
