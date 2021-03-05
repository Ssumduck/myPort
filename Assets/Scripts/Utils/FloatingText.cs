using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public static void DamageText(Transform _transform, string text, Color color, float time = 1f)
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/UI/FloatingText") as GameObject, _transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.SetParent(null);

        go.transform.localRotation = Quaternion.Euler(new Vector3(go.transform.localRotation.x, go.transform.localRotation.y + 180f, go.transform.localRotation.z));

        TextMeshPro ui = go.transform.GetChild(0).GetComponent<TextMeshPro>();

        Vector3 vec = ((ui.transform.position - Camera.main.transform.position).normalized) + ui.transform.position;

        ui.transform.LookAt(vec);

        ui.text = text;
        ui.color = color;
        ui.sortingOrder = 3000;

        Destroy(go, time);
    }
}
