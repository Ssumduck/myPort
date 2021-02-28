using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroyManagement : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            DontDestroyOnLoad(objects[i]);
        }
    }
}
