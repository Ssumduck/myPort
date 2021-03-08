using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroyManagement : MonoBehaviour
{
    static bool donDestroy = false;
    [SerializeField]
    List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        if (!donDestroy)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                DontDestroyOnLoad(objects[i]);
            }
            donDestroy = true;
        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Destroy(objects[i]);
            }
        }
    }
}
