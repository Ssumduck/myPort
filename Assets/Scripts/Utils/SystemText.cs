using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemText : MonoBehaviour
{
    static Vector3 startVec = new Vector3(0, -215);
    [SerializeField]
    static Text[] texts;

    private void Awake()
    {
        texts = GetComponentsInChildren<Text>();

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].gameObject.SetActive(false);
        }
    }

    public static void SystemMessage(string message, Color color)
    {
        Text text = TextReturn();
        text.transform.localPosition = startVec;

        text.gameObject.SetActive(true);

        text.color = color;
        text.text = message;

        text.GetComponent<Animator>().SetTrigger("Init");
    }


    static Text TextReturn()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if (!texts[i].gameObject.activeSelf)
                return texts[i];
        }
        return null;
    }
}
