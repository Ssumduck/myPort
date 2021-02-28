using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    static GameObject go;
    public static void Init()
    {
        if (go == null)
        {
            go = new GameObject("@LoadManager");
            go.AddComponent(typeof(LoadSceneManager));
            go.AddComponent(typeof(CoroutineManager));
            DontDestroyOnLoad(go);
        }
    }

    public static void Loading(string sceneName)
    {
        if (go == null)
            Init();
        
        CoroutineManager.Instance.MyStartCoroutine(LoadingScene(sceneName));
    }

    static IEnumerator LoadingScene(string sceneName)
    {
        SceneManager.LoadSceneAsync("Loading");

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        Slider slider = null;

        while (slider == null)
        {
            slider = GameObject.FindObjectOfType<Slider>();
            yield return new WaitForSeconds(0.01f);
        }

        float time = 0f;

        while (!op.isDone)
        {
            yield return null;

            time += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(slider.value, op.progress, time);

                if (slider.value >= op.progress)
                    time = 0f;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, time);
                if (slider.value == 1f)
                {
                    op.allowSceneActivation = true;
                }
            }
        }
    }
}
