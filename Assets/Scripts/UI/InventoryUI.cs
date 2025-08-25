using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private int maxSlotAmount = 20;
    private float maxWeightAmount = 40f;
    private float currentWeightAmount = 4f;
    private float currentSlotAmount = 3f;
    [SerializeField] private TextMeshProUGUI totalWeightText;
    [SerializeField] private TextMeshProUGUI totalSlotText;
    [SerializeField] private Image weightIcon;
    [SerializeField] private Image slotIcon;
    [SerializeField] private ItemUI itemUITemplate;
    [SerializeField] private Button closeButton;
    [SerializeField] private Transform inventoryList;
    private bool isContainer = false;

    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("multiples instances of inventory UI found");
        }
        Instance = this;
    }

    private void Start()
    {
        itemUITemplate.gameObject.SetActive(false);
        closeButton.onClick.AddListener(HideInventory);
        gameObject.SetActive(false);
    }

    public void DisplayInventory(Inventory inventory)
    {
        maxSlotAmount = inventory.MaxSlots;
        maxWeightAmount = inventory.MaxWeight;
        currentSlotAmount = inventory.CurrentSlots;
        currentWeightAmount = inventory.CurrentWeight;
        isContainer = inventory.IsContainer;
        UpdateSlots();
        UpdateWeight();
        InitializeItems(inventory.Items);
        gameObject.SetActive(true);
    }
    
    public void HideInventory()
    {
        gameObject.SetActive(false);
    }

    public void InitializeItems(List<Item> items)
    {
        foreach (Transform child in inventoryList)
        {
            if (child == itemUITemplate.transform) continue;
            Destroy(child.gameObject);
        }
        foreach (Item item in items)
        {
            ItemUI itemUI = Instantiate(itemUITemplate, inventoryList);
            itemUI.SetItem(item);
            itemUI.gameObject.SetActive(true);
        }
    }

    private void UpdateWeight()
    {
        totalWeightText.gameObject.SetActive(!isContainer);
        weightIcon.gameObject.SetActive(!isContainer);
        totalWeightText.text = $"{currentWeightAmount}/{maxWeightAmount}";
    }
    private void UpdateSlots()
    {
        totalSlotText.text = $"{currentSlotAmount}/{maxSlotAmount}";
    }
}
