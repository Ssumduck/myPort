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

        popup.SetActive(true);
        popup.transform.position = new Vector2(transform.position.x + 300, transform.position.y - 165);

        Button btn = popup.transform.GetChild(3).GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => UnEquipment(TYPE));
    }

    void UnEquipment(Define.EquipmentType type)
    {
        Item item = player.Equipment[type];

        ItemUtil.GetItem(item);

        player.Equipment.Remove(type);

        ItemUtil.UnEquipmentItem(item);

        equipment.ImageInit();
        popup.SetActive(false);
    }
}