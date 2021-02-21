using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    GameObject popup;

    Item slotItem = null;
    [SerializeField]
    Image slot;

    Transform startParent;
    Transform endParent;
    
    public Sprite slotBackground;

    Text countText;

    GraphicRaycaster gr;

    public void Init()
    {
        countText = transform.GetChild(2).GetComponent<Text>();
        gr = GameObject.Find("UI").GetComponent<GraphicRaycaster>();
        slot = transform.GetChild(1).GetComponent<Image>();
    }

    public void SlotInit(Item item)
    {
        if (slot == null)
            Init();
        Sprite sprite;
        if(item == null)
        {
            sprite = slotBackground;
            slot.sprite = sprite;
            slotItem = null;
            return;
        }

        if (item.ItemCount > 1)
            countText.text = $"X {item.ItemCount}";
        else
            countText.text = string.Empty;
        
        sprite = Resources.Load<Sprite>($"Image/ItemIcon/{item.Index}");
        slot.sprite = sprite;
        slotItem = item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startParent = transform;

        slot.transform.SetParent(transform.root);
        slot.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        slot.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySlot invenSlot = null;

        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(eventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.GetComponent<InventorySlot>() != null)
            {
                invenSlot = results[i].gameObject.GetComponent<InventorySlot>();
                endParent = invenSlot.transform;
                break;
            }
        }
        Transform tf = invenSlot.transform.GetChild(1);
        tf.SetParent(startParent);
        slot.gameObject.transform.SetParent(endParent);

        tf.localPosition = Vector3.zero;
        slot.transform.localPosition = Vector3.zero;

        ItemUtil.InventoryIndexChange(startParent.GetSiblingIndex(), endParent.GetSiblingIndex());
        Init();
        invenSlot.Init();
        invenSlot.SlotInit(ItemUtil.ReturnItem(endParent.GetSiblingIndex()));
        SlotInit(ItemUtil.ReturnItem(startParent.GetSiblingIndex()));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotItem == null)
            return;

        //ItemUtil.ItemInfo(slotItem, popup, transform);
        Managers.Tooltip.ToolTipCreate(transform, Define.TooltipType.ITEM, slotItem);
    }
}