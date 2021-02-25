using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool ui = false;
    Vector3 prevAngle;
    Transform arm;
    GameObject target;

    private void Start()
    {
        target = GameObject.FindObjectOfType<Player>().gameObject;
        arm = transform.root;
    }

    private void Update()
    {
        TouchRotate();
        arm.position = target.transform.position;
        transform.localPosition = new Vector3(0, 1, -4);
    }

    void TouchRotate()
    {
        if (Input.touchCount == 1 && !ui)
        {
            Touch touch = Input.GetTouch(0);

            float deltaX;
            float deltaY;


            switch (touch.phase)
            {
                case TouchPhase.Began:
                    prevAngle = touch.position;
                    break;
                case TouchPhase.Moved:
                    deltaX = touch.deltaPosition.y * Time.deltaTime;
                    deltaY = touch.deltaPosition.x * Time.deltaTime;

                    prevAngle = arm.rotation.eulerAngles;

                    deltaX = prevAngle.x - deltaX;
                    deltaY = prevAngle.y + deltaY;

                    Debug.Log(deltaX);

                    if(deltaX < 180f)
                    {
                        deltaX = Mathf.Clamp(deltaX, -1f, 70f);
                    }
                    else{
                        deltaX = Mathf.Clamp(deltaX, 347f, 361f);
                    }

                    arm.rotation = Quaternion.Euler(deltaX, deltaY, prevAngle.z);

                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ui = EventSystem.current.IsPointerOverGameObject();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}