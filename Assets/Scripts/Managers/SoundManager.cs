using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static float BGMVol = 1f;
    public static float SFXVol = 1f;

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    Dictionary<string, AudioClip> BGMList = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> SFXList = new Dictionary<string, AudioClip>();

    public void Init()
    {
        if (Managers.Sound != null)
            return;
        Transform root = GameObject.Find("@Managers").transform;

        GameObject go = GameObject.Find("@Sound");
        if (go == null)
        {
            go = new GameObject("@Sound", typeof(SoundManager));
            go.transform.SetParent(root);
        }
        Managers.Sound = go.GetComponent<SoundManager>();

        Managers.Sound.BGMSource = go.AddComponent<AudioSource>();

        Managers.Sound.BGMList = SoundInit("BGM");
        Managers.Sound.SFXList = SoundInit("SFX");
    }

    Dictionary<string, AudioClip> SoundInit(string path)
    {
        Dictionary<string, AudioClip> dic = new Dictionary<string, AudioClip>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>($"Sounds/{path}");

        for (int i = 0; i < clips.Length; i++)
        {
            dic.Add(clips[i].name, clips[i]);
        }
        return dic;
    }

    public void BGMPlay(string BGMName)
    {
        Managers.Sound.BGMSource.clip = Managers.Sound.BGMList[BGMName];
        Managers.Sound.BGMSource.volume = BGMVol;
        Managers.Sound.BGMSource.Play();
        Managers.Sound.BGMSource.loop = true;
    }

    public void SFXPlay(string SFXName)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = Managers.Sound.SFXList[SFXName];
        audio.loop = false;
        audio.volume = SFXVol;
        audio.Play();
        Destroy(audio, audio.clip.length);
    }
}