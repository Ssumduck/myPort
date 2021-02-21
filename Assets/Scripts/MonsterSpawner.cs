using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public float radius;

    [SerializeField]
    Define.MonsterType monster = Define.MonsterType.None;

    [SerializeField]
    int monsterCount;

    public float spawnTime;
    float elapsedTime = 0;

    private void Update()
    {
        if (!canSpawn)
            return;

        elapsedTime += Time.deltaTime;

        if(spawnTime < elapsedTime)
        {
            elapsedTime = 0f;

            switch (monster)
            {
                case Define.MonsterType.Warlock:
                    MonsterPool.WarlockSpawn(this, radius);
                    break;
            }
        }
    }
}
