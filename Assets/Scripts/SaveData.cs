using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public Text text;

    Player player;

    string sceneName;
    string posX, posY, posZ;

    string playerName;
    string playerLevel;
    string playerGold;
    string currEXP;
    string statPoint;
    string skillPoint;

    string STR, DEX, INT, LUK, HP, MP, currHP, currMP, AD, AP, DEF, MR;

    List<string> myQuestIndex = new List<string>();

    List<string> clearQuestIndex = new List<string>();

    List<string> inventoryItemIndex = new List<string>();
    List<string> inventorySlotIndex = new List<string>();
    List<string> inventoryItemCount = new List<string>();

    List<string> skillIndex = new List<string>();
    List<string> skillLevel = new List<string>();

    void Init()
    {
        player = GameObject.FindObjectOfType<Player>();

        sceneName = SceneManager.GetActiveScene().name;

        posX = player.transform.position.x.ToString();
        posY = player.transform.position.y.ToString();
        posZ = player.transform.position.z.ToString();

        playerName = player.myStat.Name;

        playerLevel = player.myStat.Level.ToString();
        playerGold = player.myStat.Gold.ToString();
        currEXP = player.myStat.currEXP.ToString();
        statPoint = player.myStat.statPoint.ToString();
        skillPoint = player.myStat.skillPoint.ToString();

        STR = player.myStat.STR.ToString();
        DEX = player.myStat.DEX.ToString();
        INT = player.myStat.INT.ToString();
        LUK = player.myStat.LUK.ToString();
        HP = player.myStat.HP.ToString();
        currHP = player.myStat.currHP.ToString();
        MP = player.myStat.MP.ToString();
        currMP = player.myStat.currMP.ToString();
        AD = player.myStat.AD.ToString();
        AP = player.myStat.AP.ToString();
        DEF = player.myStat.DEF.ToString();
        MR = player.myStat.MR.ToString();


        for (int i = 0; i < player.myQuest.Count; i++)
        {
            myQuestIndex.Add(player.myQuest[i].Index.ToString());
        }

        for (int i = 0; i < Managers.Data.quests.Count; i++)
        {
            if (Managers.Data.quests[i].Clear)
                clearQuestIndex.Add(Managers.Data.quests[i].Index.ToString());
        }

        for (int i = 0; i < player.inven.items.Count; i++)
        {
            inventoryItemIndex.Add(player.inven.items[i].Index.ToString());
            inventorySlotIndex.Add(player.inven.items[i].inventoryIndex.ToString());
            inventoryItemCount.Add(player.inven.items[i].ItemCount.ToString());
        }

        skillIndex = player.mySkillReturns().Item1;
        skillLevel = player.mySkillReturns().Item2;

        Debug.Log("초기화 완료");
    }

    void Save()
    {
        FileStream file;
        if(File.Exists(Application.persistentDataPath + "/SaveData/SaveFile.txt"))
        {
            file = new FileStream(Application.persistentDataPath + "/SaveData/SaveFile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            goto FileFind;
        }

        Directory.CreateDirectory(Application.persistentDataPath + "/" + "SaveData");
        file = new FileStream(Application.persistentDataPath + "/" + "SaveData/SaveFile.txt", FileMode.Create, FileAccess.Write);

        FileFind:

        StreamWriter sw = new StreamWriter(file, Encoding.UTF8);

        string[] data = new string[21] {playerName, playerLevel, playerGold, statPoint, skillPoint, STR, DEX, INT, LUK, HP,
                                        currHP, MP, currMP, AD, AP, DEF, MR, sceneName, posX, posY,
                                        posZ};

        for (int i = 0; i < data.Length; i++)
        {
            sw.Write(data[i] + '\t');
        }

        for (int i = 0; i < myQuestIndex.Count; i++)
            sw.Write(myQuestIndex[i] + ',');

        sw.Write('\t');

        for (int i = 0; i < clearQuestIndex.Count; i++)
            sw.Write(clearQuestIndex[i] + ',');

        sw.Write('\t');

        for (int i = 0; i < inventoryItemCount.Count; i++)
            sw.Write(inventoryItemCount[i] + ',');

        sw.Write('\t');

        for (int i = 0; i < inventorySlotIndex.Count; i++)
            sw.Write(inventorySlotIndex[i] + ',');

        sw.Write('\t');

        for (int i = 0; i < inventoryItemCount.Count; i++)
            sw.Write(inventoryItemCount[i] + ',');

        sw.Write('\t');

        for (int i = 0; i < skillIndex.Count; i++)
            sw.Write(skillIndex[i] + ',');

        sw.Write('\t');

        for (int i = 0; i < skillLevel.Count; i++)
            sw.Write(skillLevel[i] + ',');

        sw.Close();
        sw.Dispose();

        Debug.Log("저장 완료");
    }

    public void LoadFunc()
    {
        string name = GameObject.FindObjectOfType<InputField>().text;

        if (String.IsNullOrEmpty(name))
        {
            return;
        }

        if (File.Exists(Application.persistentDataPath + "/" + "SaveData/SaveFile.txt"))
        {
            text.text = "파일을 찾았습니다.";
            StartCoroutine(Load(name));
        }
        else
        {
            text.text = "파일이 없습니다.";
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
            FileStream file = new FileStream(Application.persistentDataPath + "/" + "SaveData/SaveFile.txt", FileMode.Create, FileAccess.Write);
            text.text = "파일 작성완료.";
            StreamWriter sw = new StreamWriter(file);

            TextAsset asset = Resources.Load<TextAsset>("Data/DefaultGame");

            string[] data = asset.text.Split('\n');

            sw.Write(data[0]);
            sw.Close();
            sw.Dispose();

            text.text = "디폴트 파일 설정완료.";

            LoadFunc();
        }
    }

    IEnumerator Load(string _name)
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadSceneAsync("Loading");

        FileStream file = new FileStream(Application.persistentDataPath + "/" + "SaveData/SaveFile.txt", FileMode.Open, FileAccess.Read);

        StreamReader read = new StreamReader(file);

        string[] data = read.ReadToEnd().Split('\t');

        // [0] PlayerName [1] Level [2] Gold [3] StatPoint [4] SkillPoint [5] STR [6] DEX [7] INT [8] LUK [9] HP [10] currHP
        // [11] MP [12] currMP [13] AD [14] AP [15] DEF [16] MR [17] SceneName [18] PosX [19] PosY [20] PosZ
        // List ',' [21] myQuestIndex [22] clearQuestIndex [23] inventoryItemIndex [24] inventorySlotIndex [25] inventoryItemCount [26] skillIndex [27] skillLevel

        AsyncOperation op = SceneManager.LoadSceneAsync(data[17]);
        op.allowSceneActivation = false;

        Slider slider = null;

        while(slider == null)
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
                    while (true)
                    {
                        Player _player = GameObject.FindObjectOfType<Player>();
                        if (_player != null)
                        {
                            _player.myStat.Level = int.Parse(data[1]);
                            _player.transform.position = new Vector3(float.Parse(data[18]), float.Parse(data[19]), float.Parse(data[20]));
                            _player.myStat.Name = _name;
                            yield break;
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        Init();
        Save();
    }
}
