using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    bool drag;

    RectTransform rectBackground;
    RectTransform rectJoystick;

    Player player;

    public static Vector3 moveVec;

    float radius;

    public bool Drag { get { return drag; } set { drag = value; } }
    public RectTransform RectJoy { get { return rectJoystick; } set { rectJoystick = value; } }

    private void Awake()
    {
        rectBackground = GetComponent<RectTransform>();
        rectJoystick = transform.GetChild(0).GetComponent<RectTransform>();

        player = GameObject.FindObjectOfType<Player>();

        radius = rectBackground.rect.width / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        drag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        drag = false;
        rectJoystick.localPosition = Vector3.zero;
        moveVec = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!drag)
            return;
        Vector2 vec = eventData.position - (Vector2)rectBackground.position;
        vec = Vector2.ClampMagnitude(vec, radius);
        rectJoystick.localPosition = vec;

        Vector3 norVec = vec.normalized;
        moveVec = new Vector3(norVec.x * player.myStat.moveSpeed * Time.deltaTime, 0f, norVec.y * player.myStat.moveSpeed * Time.deltaTime);
        Vector3 lookVec = new Vector3(norVec.x + player.transform.position.x, player.transform.position.y, norVec.y + player.transform.position.z);
        player.transform.LookAt(lookVec);
    }

    private void Update()
    {
        if (player.canMove)
        {
            if (drag && player.State != Define.PlayerState.Attack)
            {
                player.State = Define.PlayerState.Moving;
            }
            else
            {
                player.State = Define.PlayerState.Idle;
            }
        }
    }
}