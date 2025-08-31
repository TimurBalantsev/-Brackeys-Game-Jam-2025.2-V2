using System.Collections.Generic;
using UnityEngine;

public class InGameEventUI : MonoBehaviour
{
    [SerializeField] private Transform eventsContainer;

    [SerializeField] private QuestUI eventUITemplate;

    private void Start()
    {
        eventUITemplate.gameObject.SetActive(false);
        InitializeEvents(BaseManager.Instance.ActiveEvents);
    }

    public void InitializeEvents(List<Quest> events)
    {
        foreach (Transform child in eventsContainer)
        {
            if (child == eventUITemplate.transform) continue;
            Destroy(child.gameObject);
        }
        foreach (Quest quest in events)
        {
            QuestUI eventUI = Instantiate(eventUITemplate, eventsContainer);
            eventUI.SetQuest(quest);
            eventUI.gameObject.SetActive(true);
        }
    }
}
