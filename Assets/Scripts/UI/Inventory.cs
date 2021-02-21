using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject slots;
    [SerializeField]
    Sprite defaultSprite;

    public List<Item> items = new List<Item>();

    private void OnEnable()
    {
        for (int i = 0; i < slots.transform.childCount; i++)
        {
            Image slot = slots.transform.GetChild(i).GetChild(1).GetComponent<Image>();
            InventorySlot Slot = slot.transform.parent.GetComponent<InventorySlot>();
            Slot.slotBackground = defaultSprite;
            slot.sprite = defaultSprite;
        }
        SlotInit();
    }

    public void SlotInit()
    {
        for (int i = 0; i < items.Count; i++)
        {
            int index = items[i].InventoryIndex;

            InventorySlot slot = slots.transform.GetChild(index).GetComponent<InventorySlot>();
            slot.SlotInit(items[i]);
        }
    }
}