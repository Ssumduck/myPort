using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance = new Managers();
    public static Managers Instance { get { instance.Init(); return instance; } }

    SoundManager sound = new SoundManager();
    public static SoundManager Sound { get { instance.Init(); return instance.sound; } set { instance.sound = value; } }

    static GameDataManager data = new GameDataManager();
    public static GameDataManager Data { get { instance.Init(); return data; } }

    static ToolTipManager tooltip = new ToolTipManager();
    public static ToolTipManager Tooltip { get { instance.Init(); return tooltip; } }


    public void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            instance = go.GetComponent<Managers>();
            sound.Init();
            data.Init();
            tooltip.Init();
        }
    }
}