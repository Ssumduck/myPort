using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContent : MonoBehaviour
{
    Vector3 startPoint = new Vector3(3.42f, 5.98f, -16.95f);

    public void DungeonExpress()
    {
        Managers.Tooltip.ToolTipCreate(transform, Define.TooltipType.DUNGEON);
    }

    public void BossRoom()
    {
        LoadSceneManager.Loading("BOSS");

        GameDataManager.player.transform.position = startPoint;
    }
}
