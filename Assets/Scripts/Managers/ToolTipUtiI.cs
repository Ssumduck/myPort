using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager
{
    Canvas canvas;
    GameObject go;
    GameObject systemGo;

    public void Init()
    {
        if (go == null)
        {
            go = Resources.Load<GameObject>("Prefabs/UI/Tooltip");
            canvas = Resources.Load<Canvas>("EmptyCanvas");
        }
    }

    public void ToolTipCreate(Transform tf, Define.TooltipType type, Item item = null, Skill skill = null)
    {
        Tooltip tooltip = Managers.Instantiate(Managers.Tooltip.go, tf.parent.parent).GetComponent<Tooltip>();
        if(tooltip.transform.root.GetComponent<Canvas>() == null)
        {
            canvas = Managers.Instantiate(canvas);
            tooltip.transform.SetParent(canvas.transform);
        }
        tooltip.type = type;

        tooltip.nameTxt = tooltip.transform.GetChild(0).GetComponent<Text>();
        tooltip.typeTxt = tooltip.transform.GetChild(1).GetComponent<Text>();
        tooltip.explainTxt = tooltip.transform.GetChild(2).GetComponent<Text>();

        tooltip.btn0 = tooltip.transform.GetChild(3).GetComponent<Button>();
        tooltip.applyTxt = tooltip.btn0.transform.GetChild(0).GetComponent<Text>();
        tooltip.btn1 = tooltip.transform.GetChild(4).GetComponent<Button>();

        tooltip.transform.position = new Vector2(tf.transform.position.x + 310, tf.transform.position.y - 165);

        switch (type)
        {
            case Define.TooltipType.ITEM:
                tooltip.item = item;
                tooltip.invenSlot = tf.GetComponent<InventorySlot>();
                break;
            case Define.TooltipType.SKILL:
                tooltip.skill = skill;
                break;
        }
        tooltip.Init();
        tooltip.transform.SetAsLastSibling();
    }

    public void SystemToolTip(string title, string context)
    {
        systemGo = Object.Instantiate<GameObject>(Resources.Load("Prefabs/UI/PopupCanvas") as GameObject);

        Transform parent = systemGo.transform.GetChild(0);

        parent.GetChild(0).GetComponent<Text>().text = title;
        parent.GetChild(1).GetComponent<Text>().text = context;
    }
}
