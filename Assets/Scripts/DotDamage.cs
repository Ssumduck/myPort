using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamage : MonoBehaviour
{
    MyStat stat;

    Define.DOTType type = Define.DOTType.NONE;

    List<Material> material = new List<Material>();
    Color color;
    Color defaultColor;

    float elapsedTime = 0f;
    float dmgElapsed = 0f;
    public int dmg;
    public float time;
    public float dmgTime;

    private void Awake()
    {
        if(GetComponent<Player>() != null)
            stat = GetComponent<Player>().myStat;

        if(stat == null)
            stat = GetComponent<Monster>().myStat;

        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            material.Add(renderers[i].material);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < material.Count; i++)
        {
            material[i].color = defaultColor;
        }
    }

    public void Init(int _dmg, float _time, Define.DOTType _type , float _dmgTime = 5f)
    {
        type = _type;
        dmg = _dmg;
        time = _time;
        dmgTime = _dmgTime;

        switch (type)
        {
            case Define.DOTType.NONE:
                color = Color.white;
                break;
            case Define.DOTType.BURN:
                color = Color.red;
                break;
            case Define.DOTType.POISON:
                color = Color.green;
                break;
        }

        for (int i = 0; i < material.Count; i++)
        {
            defaultColor = material[i].color;
            material[i].color = color;
        }
    }

    private void Update()
    {
        dmgElapsed += Time.deltaTime;

        if(dmgElapsed > dmgTime)
        {
            elapsedTime += dmgElapsed;
            dmgElapsed = 0f;

            stat.currHP -= dmg;

            GameObject go = Instantiate(Resources.Load($"DOTParticle/{type.ToString()}") as GameObject, transform);
            Destroy(go, 1.5f);

            if(elapsedTime >= time)
            {
                Destroy(this);
            }
        }
    }
}
