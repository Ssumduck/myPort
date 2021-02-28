using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance { get { return instance; } }

    static CoroutineManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public void MyStartCoroutine(IEnumerator func)
    {
        StartCoroutine(func);
    }
}
