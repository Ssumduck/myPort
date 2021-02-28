using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("AttackBtn").GetComponent<Button>().onClick.AddListener(() => GameDataManager.player.AttackBtn());
    }
}
