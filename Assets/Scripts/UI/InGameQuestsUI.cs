using System.Collections.Generic;
using UnityEngine;

public class InGameQuestsUI : MonoBehaviour
{
    [SerializeField] private Transform questsContainer;

    [SerializeField] private QuestUI questUITemplate;

    private void Start()
    {
        questUITemplate.gameObject.SetActive(false);
        InitializeQuests(BaseManager.Instance.ActiveQuests);
    }

    public void InitializeQuests(List<Quest> quests)
    {
        foreach (Transform child in questsContainer)
        {
            if (child == questUITemplate.transform) continue;
            Destroy(child.gameObject);
        }
        foreach (Quest quest in quests)
        {
            QuestUI questUI = Instantiate(questUITemplate, questsContainer);
            questUI.SetQuest(quest);
            questUI.gameObject.SetActive(true);
        }
    }
}
