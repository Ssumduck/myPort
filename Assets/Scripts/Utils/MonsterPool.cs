using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    static MonsterPool instance;

    [SerializeField]
    GameObject warlock_Prefab;
    [SerializeField]
    int warlock_Count;
    [SerializeField]
    public static Queue<GameObject> warlocks = new Queue<GameObject>();

    private void Start()
    {
        instance = this;
        WarlockInit();
    }

    void WarlockInit()
    {
        for (int i = 0; i < warlock_Count; i++)
        {
            GameObject warlock = Instantiate(warlock_Prefab, transform);
            warlock.gameObject.SetActive(false);
            warlocks.Enqueue(warlock);
        }
    }

    public static void WarlockSpawn(MonsterSpawner spawner, float radius)
    {
        Warlock go = warlocks.Dequeue().GetComponent<Warlock>();
        go.gameObject.SetActive(true);

        go.transform.SetParent(null);
        go.transform.position = new Vector3(RandomUtil.RandomVector3(spawner.transform, radius).x, spawner.transform.position.y, RandomUtil.RandomVector3(spawner.transform, radius).z);

        go.spawner = spawner;


        if (warlocks.Count <= 0)
            spawner.canSpawn = false;
    }

    public static void DieMonster(Monster monster)
    {
        if (instance == null)
            return;

        monster.gameObject.SetActive(false);
        monster.transform.SetParent(instance.transform);
        monster.transform.localPosition = Vector3.zero;

        switch (monster.type)
        {
            case Define.MonsterType.Warlock:
                warlocks.Enqueue(monster.gameObject);
                instance.StartCoroutine(instance.SpawnCoroutine(monster));
                break;
        }
    }

    IEnumerator SpawnCoroutine(Monster monster)
    {
        yield return new WaitForSeconds(monster.spawner.spawnTime);

        switch (monster.type)
        {
            case Define.MonsterType.Warlock:
                WarlockSpawn(monster.spawner, monster.spawner.radius);
                break;
        }

    }
}