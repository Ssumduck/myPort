using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUtil : MonoBehaviour
{
    public static NPC NPCIndexReturn(int index)
    {
        NPC[] data = GameObject.FindObjectsOfType<NPC>();

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].index == index)
                return data[i];
        }
        return null;
    }
}
