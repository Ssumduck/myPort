using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    Player player;

    [SerializeField]
    Sprite defaultImg;

    [SerializeField]
    Image helmatImg;
    [SerializeField]
    Image weaponImg;
    [SerializeField]
    Image gloveImg;
    [SerializeField]
    Image shoseImg;
    [SerializeField]
    Image shieldImg;
    [SerializeField]
    Image ringImg;

    private void OnEnable()
    {
        ImageInit();
    }

    public void ImageInit()
    {
        if (player == null)
            player = GameObject.FindObjectOfType<Player>();

        helmatImg.sprite = defaultImg;
        weaponImg.sprite = defaultImg;
        gloveImg.sprite = defaultImg;
        shieldImg.sprite = defaultImg;
        shoseImg.sprite = defaultImg;
        ringImg.sprite = defaultImg;

        if (player.Equipment.ContainsKey(Define.EquipmentType.Helmat))
            helmatImg.sprite = Resources.Load<Sprite>($"Image/ItemIcon/{player.Equipment[Define.EquipmentType.Helmat].Index}");
        if (player.Equipment.ContainsKey(Define.EquipmentType.Weapon))
            weaponImg.sprite = Resources.Load<Sprite>($"Image/ItemIcon/{player.Equipment[Define.EquipmentType.Weapon].Index}");
        if (player.Equipment.ContainsKey(Define.EquipmentType.Glove))
            gloveImg.sprite = Resources.Load<Sprite>($"Image/ItemIcon/{player.Equipment[Define.EquipmentType.Glove].Index}");
        if (player.Equipment.ContainsKey(Define.EquipmentType.Shose))
            shoseImg.sprite = Resources.Load<Sprite>($"Image/ItemIcon/{player.Equipment[Define.EquipmentType.Shose].Index}");
        if (player.Equipment.ContainsKey(Define.EquipmentType.Ring))
            ringImg.sprite = Resources.Load<Sprite>($"Image/ItemIcon/{player.Equipment[Define.EquipmentType.Ring].Index}");

        Debug.Log("로드완료");
    }
}
