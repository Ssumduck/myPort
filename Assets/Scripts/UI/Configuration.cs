using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{
    [SerializeField]
    public Slider bgmSlider;
    [SerializeField]
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = SoundManager.BGMVol;
        sfxSlider.value = SoundManager.SFXVol;
    }

    private void Update()
    {
        Managers.Sound.BGMSource.volume = bgmSlider.value;
        SoundManager.SFXVol = sfxSlider.value;
        SoundManager.BGMVol = bgmSlider.value;
    }
}
