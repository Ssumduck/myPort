using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    [SerializeField]
    Image bgImg;
    [SerializeField]
    Text clearTimeTxt, hitCountTxt;
    [SerializeField]
    Button returnBtn;

    [SerializeField]
    RectTransform unityChan;

    float time;
    int hitCount;

    public void Init(float _time, int _hitCount)
    {
        gameObject.SetActive(true);
        time = _time;
        hitCount = _hitCount;
        StartCoroutine(BackGroundAlpha());
    }

    IEnumerator BackGroundAlpha()
    {
        yield return new WaitForSeconds(0.1f);
        while (bgImg.color.a < 1f)
        {
            bgImg.color = new Color(bgImg.color.r, bgImg.color.g, bgImg.color.b, bgImg.color.a + 0.05f);
            yield return new WaitForSeconds(0.004f);
        }
        StartCoroutine(UnityChanMove());
    }

    IEnumerator UnityChanMove()
    {
        while (unityChan.transform.localPosition.x > -550)
        {
            unityChan.transform.localPosition -= Vector3.right * 20f;
            yield return new WaitForSeconds(0.0005f);
        }

        StartCoroutine(Result(time, hitCount));
    }

    IEnumerator Result(float _time, int _hitCount)
    {
        int min = 0, seconds = 0;

        while (_time > 0)
        {
            if(_time >= 60)
            {
                _time -= 60;
                min += 1;
            }
            else
            {
                seconds = (int)_time;
                _time = 0;
            }
        }

        clearTimeTxt.text = $"클리어 타임 : {min.ToString("D2")} : {seconds.ToString("D2")}";
        hitCountTxt.text = $"피격 횟수 : {_hitCount}";

        RectTransform ct = clearTimeTxt.GetComponent<RectTransform>();
        RectTransform hc = hitCountTxt.GetComponent<RectTransform>();

        while (ct.transform.localPosition.y > 170)
        {
            ct.transform.localPosition -= Vector3.up * 10f;
            hc.transform.localPosition -= Vector3.up * 10f;
            yield return new WaitForSeconds(0.0005f);
        }
        returnBtn.gameObject.SetActive(true);
        returnBtn.onClick.RemoveAllListeners();
        returnBtn.onClick.AddListener(() => LoadSceneManager.Loading("Game"));
        returnBtn.onClick.AddListener(() => GameDataManager.player.transform.position = new Vector3(0, 0.07f, 0));
    }
}