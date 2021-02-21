using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = Managers.Sound.BGMVol;
        sfxSlider.value = Managers.Sound.SFXVol;
    }

    private void Update()
    {
        Managers.Sound.BGMSource.volume = bgmSlider.value;
        Managers.Sound.SFXVol = sfxSlider.value;
    }
}
