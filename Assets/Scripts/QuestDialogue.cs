using UnityEngine;
using UnityEngine.UI;

public class QuestDialogue : MonoBehaviour
{
    Player player;
    Quest quest;
    [SerializeField]
    QuestList list;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text explainText;
    [SerializeField]
    Text[] aceeptText;
    [SerializeField]
    Button acceptBtn;
    [SerializeField]
    Button closeBtn;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void Init(Quest _quest, Define.QuestState state)
    {
        acceptBtn.onClick.RemoveAllListeners();

        gameObject.SetActive(true);
        list.gameObject.SetActive(false);

        nameText.text = _quest.Name;
        explainText.text = _quest.Explain;
        quest = _quest;

        switch (state)
        {
            case Define.QuestState.NONE:
                aceeptText[0].text = "수 락";
                aceeptText[1].text = "거 절";
                acceptBtn.onClick.AddListener(Accept);
                break;
            case Define.QuestState.Accept:
                aceeptText[0].text = "포 기";
                aceeptText[1].text = "닫 기";
                break;
            case Define.QuestState.COMPLETE:
                aceeptText[0].text = "완 료";
                aceeptText[1].text = "닫 기";
                acceptBtn.onClick.AddListener(() => quest.QuestClear(quest));
                break;
        }
        acceptBtn.onClick.AddListener(Close);
    }

    public void Accept()
    {
        Quest _quest = Quest.QuestClone(quest);
        _quest.accept = true;
        NPC npc = NPCUtil.NPCIndexReturn(quest.NPCIndex);

        player.myQuest.Add(_quest);

        npc.QuestAccept(quest);
    }

    void QuestCancel()
    {
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
