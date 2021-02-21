using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCFinder : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject atkBtn;
    [SerializeField]
    GameObject talkBtn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            player.nearNPC = other.GetComponent<NPC>();
            atkBtn.SetActive(false);
            talkBtn.SetActive(true);
            talkBtn.GetComponent<Image>().sprite = player.nearNPC.btnSprite;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (player.nearNPC.questList != null)
            {
                player.nearNPC.questList.gameObject.SetActive(false);
            }
            player.nearNPC = null;
            atkBtn.SetActive(true);
            talkBtn.SetActive(false);
        }
    }
}
