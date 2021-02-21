using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtil : MonoBehaviour
{
    public static Vector3 RandomVector3(Transform center, float radius)
    {
        Vector3 vec = (Random.insideUnitSphere * radius ) + center.transform.position;
        return vec;
    }
}
