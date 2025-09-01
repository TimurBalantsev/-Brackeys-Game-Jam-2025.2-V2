using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject statusContainer;
    [SerializeField] private GameObject nextContainer;

    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private Transform populationContainer;
    [SerializeField] private GameObject populationPrefab;

    [SerializeField] private Transform questsContainer;
    [SerializeField] private Transform eventsContainer;

    [SerializeField] private QuestUI questUITemplate;
    [SerializeField] private QuestUI eventUITemplate;

    [SerializeField] private Button statusContinueButton;
    [SerializeField] private Button nextContinueButton;

    private void Awake()
    {
        statusContinueButton.onClick.AddListener(ShowNext);
        nextContinueButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        LoadingManager.Instance.OnLoadBase += Instance_OnBackToBase;
    }

    private void OnDestroy()
    {
        LoadingManager.Instance.OnLoadBase -= Instance_OnBackToBase;
    }

    private void Instance_OnBackToBase()
    {
        background.SetActive(true);
        ShowStatus();
    }

    private void ShowStatus()
    {
        statusContainer.SetActive(true);
        nextContainer.SetActive(false);

        inventoryUI.DisplayInventory(BaseManager.Instance.Inventory);

        foreach (Transform child in populationContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < BaseManager.Instance.Population; i++)
        {
            Instantiate(populationPrefab, populationContainer);
        }
    }

    private void ShowNext()
    {
        statusContainer.SetActive(false);
        nextContainer.SetActive(true);

        questUITemplate.gameObject.SetActive(false);
        eventUITemplate.gameObject.SetActive(false);

        InitializeQuests(BaseManager.Instance.ActiveQuests);
        InitializeEvents(BaseManager.Instance.ActiveEvents);
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

    private void Close()
    {
        background.SetActive(false);
        LoadingManager.Instance.LoadLevelAfterBase();
    }
}
