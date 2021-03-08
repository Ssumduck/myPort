using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSROOM : MonoBehaviour
{
    static Reward reward;

    static float clearTime = 0f;
    static int hitCount = 0;
    public static bool clear = false;
    void Start()
    {
        clearTime = 0f;
        hitCount = 0;
        reward = GameObject.FindObjectOfType<Reward>();
        reward.gameObject.SetActive(false);
        Managers.Sound.BGMPlay("BOSS");
    }

    private void Update()
    {
        clearTime += Time.deltaTime;
    }

    public static void Hit()
    {
        hitCount += 1;
    }

    public static void BossClear()
    {
        while (Time.timeScale > 0.1f)
        {
            Time.timeScale -= 0.05f;
            Time.fixedDeltaTime -= 0.05f;
        }

        reward.Init(clearTime, hitCount);
    }
}
