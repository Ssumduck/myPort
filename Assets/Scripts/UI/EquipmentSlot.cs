using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    Player player;
    Equipment equipment;
    [SerializeField]
    GameObject popup;
    [SerializeField]
    Define.EquipmentType TYPE = Define.EquipmentType.NONE;

    public Define.EquipmentType Type { get { return TYPE; } }

    void Init()
    {
        player = GameObject.FindObjectOfType<Player>();
        equipment = transform.parent.GetComponent<Equipment>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player == null)
            Init();

        if (!player.Equipment.ContainsKey(TYPE))
            return;

        Managers.Tooltip.ToolTipCreate(transform, Define.TooltipType.EQUIPMENT, player.Equipment[TYPE]);
    }

    public void UnEquipment(Define.EquipmentType type)
    {
        Item item = player.Equipment[type];

        ItemUtil.GetItem(item);

        player.Equipment.Remove(type);

        ItemUtil.UnEquipmentItem(item);

        equipment.ImageInit();
        popup.SetActive(false);
    }
}