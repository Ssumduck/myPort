using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public static Queue<GameObject> uiQueue = new Queue<GameObject>();

    public void CloseButton()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        go.SetActive(false);
    }

    public void UIbutton(GameObject go)
    {
        uiQueue.Enqueue(go);
        go.SetActive(true);
        go.transform.SetAsLastSibling();
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}