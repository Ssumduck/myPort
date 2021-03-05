using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField]
    Define.TrapType type = Define.TrapType.NONE;

    [SerializeField]
    Monster[] SpawnPrefab;

    [SerializeField]
    Transform[] SpawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trap();
        }
    }

    void Trap()
    {
        switch (type)
        {
            case Define.TrapType.SPAWN:
                for (int i = 0; i < SpawnPos.Length; i++)
                {
                    Monster mon = Instantiate(SpawnPrefab[i], SpawnPos[i]);
                    mon.State = Define.MonsterState.TRAP;
                    mon.AttackTarget = GameDataManager.player;
                }
                break;
        }
    }
}
