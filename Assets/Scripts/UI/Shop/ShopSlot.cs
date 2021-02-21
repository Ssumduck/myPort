using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject tooltip;
    Image itemIcon;

    public void Init(Item item)
    {
        if(itemIcon == null)
            itemIcon = transform.GetChild(0).GetComponent<Image>();

        gameObject.SetActive(true);
        itemIcon.sprite = Resources.Load<Sprite>($"Image/ItemIcon/{item.Index}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Item item = Item.Clone(Managers.Data.ItemData[int.Parse(itemIcon.sprite.name)]);
        item.Type = Define.ItemType.BUY;
        Managers.Tooltip.ToolTipCreate(transform, Define.TooltipType.ITEM, item);
    }
}