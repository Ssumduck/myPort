using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public static void DamageText(Transform _transform, string text, Color color, float time = 1f)
    {
        _transform.LookAt(Camera.main.transform);

        GameObject go = Instantiate(Resources.Load("Prefabs/UI/FloatingText") as GameObject, _transform);

        TextMeshPro ui = go.GetComponent<TextMeshPro>();

        ui.text = text;
        ui.color = color;

        Quaternion qu = Quaternion.Euler(ui.transform.localRotation.x, ui.transform.localRotation.y + 180f, ui.transform.localRotation.z);
        ui.transform.localRotation = qu;

        Destroy(ui.gameObject, time);
    }
}
