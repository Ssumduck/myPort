using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamage : MonoBehaviour
{
    MyStat stat;

    List<Material> material = new List<Material>();

    float elapsedTime = 0f;
    float dmgElapsed = 0f;
    public int dmg;
    public float time;
    public float dmgTime;

    private void Awake()
    {
        stat = GetComponent<Player>().myStat;

        if(stat == null)
            stat = GetComponent<Monster>().myStat;

        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            material.Add(renderers[i].material);
        }

        Init(10, 5);
    }

    private void OnDisable()
    {
        for (int i = 0; i < material.Count; i++)
        {
            material[i].color = Color.white;
        }
    }

    public void Init(int _dmg, float _time, float _dmgTime = 5f)
    {
        dmg = _dmg;
        time = _time;
        dmgTime = _dmgTime;

        for (int i = 0; i < material.Count; i++)
        {
            material[i].color = Color.red;
        }
    }

    private void Update()
    {
        dmgElapsed += Time.deltaTime;

        if(dmgElapsed > dmgTime)
        {
            elapsedTime = dmgElapsed;
            dmgElapsed = 0f;

            stat.currHP -= dmg;

            if(elapsedTime > time)
            {
                Destroy(this);
            }
        }
    }
}
