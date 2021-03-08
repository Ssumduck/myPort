using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    Player player;

    [SerializeField]
    Text nameText;
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text strText;
    [SerializeField]
    Text dexText;
    [SerializeField]
    Text intText;
    [SerializeField]
    Text lukText;
    [SerializeField]
    Text statPointText;
    [SerializeField]
    Text atkText;
    [SerializeField]
    Text defText;

    [SerializeField]
    Button strBtn;
    [SerializeField]
    Button dexBtn;
    [SerializeField]
    Button intBtn;
    [SerializeField]
    Button lukBtn;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        strBtn.onClick.AddListener(() => StatUp(player, 0));
        dexBtn.onClick.AddListener(() => StatUp(player, 1));
        intBtn.onClick.AddListener(() => StatUp(player, 2));
        lukBtn.onClick.AddListener(() => StatUp(player, 3));
    }

    private void OnEnable()
    {
        StatusInit();
    }

    void StatusInit()
    {
        nameText.text = $"캐릭터 이름 : {player.myStat.Name}";
        levelText.text = $"레벨 : {player.myStat.Level} ({player.myStat.currEXP} / {player.myStat.EXP})";
        strText.text = $"힘 : {player.myStat.STR}";
        dexText.text = $"민첩 : {player.myStat.DEX}";
        intText.text = $"지력 : {player.myStat.INT}";
        lukText.text = $"행운 : {player.myStat.LUK}";
        statPointText.text = $"스텟 포인트 : {player.myStat.statPoint}";
        atkText.text = $"공격력 : {player.myStat.AD}";
        defText.text = $"방어력 : {player.myStat.DEF}";

        if (player.Equipment.ContainsKey(Define.EquipmentType.Weapon))
            atkText.text = $"공격력 : {player.myStat.AD} ( {player.myStat.AD - player.Equipment[Define.EquipmentType.Weapon].AD} + {player.Equipment[Define.EquipmentType.Weapon].AD} )";
        if(player.Equipment.ContainsKey(Define.EquipmentType.Armor))
            defText.text = $"방어력 : {player.myStat.AD} ( {player.myStat.DEF - player.Equipment[Define.EquipmentType.Armor].DEF} + {player.Equipment[Define.EquipmentType.Armor].DEF} )";
    }

    public void StatUp(Player player, int index)
    {
        if(player.myStat.statPoint > 0)
        {
            player.myStat.statPoint -= 1;

            switch (index)
            {
                case 0:
                    player.myStat.STR += 1;
                    player.myStat.AD += 1;
                    break;
                case 1:
                    player.myStat.DEX += 1;
                    player.myStat.DEF += 1;
                    break;
                case 2:
                    player.myStat.INT += 1;
                    break;
                case 3:
                    player.myStat.LUK += 1;
                    break;
            }
            StatusInit();
        }
    }
}