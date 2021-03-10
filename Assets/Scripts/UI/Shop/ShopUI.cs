using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Transform content;
    [SerializeField]
    ShopSlot[] slots;
    [SerializeField]
    Text goldText;

    public void Init(NPC npc)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            for (int i = 0; i < npc.shopItem.Count; i++)
            {
                slots[i].Init(npc.shopItem[i]);
            }
        }
    }

    public void GoldText()
    {
        string data = player.myStat.Gold.ToString();

        if (data.Length > 3)
        {
            string temp = new string(data.ToCharArray().Reverse().ToArray());

            for (int i = temp.Length - 1; i > 1; i--)
            {
                if (i % 3 == 0)
                {
                    StringBuilder str = new StringBuilder(temp);
                    str.Insert(i, ",");
                    temp = str.ToString();
                }
            }
            data = new string(temp.ToCharArray().Reverse().ToArray());
        }

        goldText.text = data;
    }

    private void OnEnable()
    {
        GoldText();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
}
