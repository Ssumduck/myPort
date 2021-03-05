using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    CameraController cameraController;

    private void Start()
    {
        if (GetComponent<Joystick>() != null || transform.parent.GetComponent<Joystick>() != null)
            Destroy(this);

        cameraController = GameObject.FindObjectOfType<CameraController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cameraController.ui = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cameraController.ui = false;
    }
}
