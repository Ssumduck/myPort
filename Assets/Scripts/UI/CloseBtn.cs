using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBtn : MonoBehaviour
{
    public Define.CloseBtn type;
    void Start()
    {
        switch (type)
        {
            case Define.CloseBtn.ACTIVE:
                GetComponent<Button>().onClick.AddListener(() => GameObject.FindObjectOfType<UIButton>().CloseButton());
                break;
            case Define.CloseBtn.DESTROY:
                GetComponent<Button>().onClick.AddListener(() => Destroy(transform.root.gameObject));
                break;
        }
    }
}
