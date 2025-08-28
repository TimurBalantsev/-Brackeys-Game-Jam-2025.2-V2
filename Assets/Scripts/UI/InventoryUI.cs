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
    [SerializeField] private HorizontalLayoutGroup parametersContainer;
    [SerializeField] private TextMeshProUGUI totalWeightText;
    [SerializeField] private TextMeshProUGUI totalSlotText;
    [SerializeField] private Image weightIcon;
    [SerializeField] private Image slotIcon;
    [SerializeField] private ItemUI itemUITemplate;
    [SerializeField] private Button closeButton;
    [SerializeField] private Transform inventoryList;
    [SerializeField] private TextMeshProUGUI inventoryName;

    private Inventory inventory;
    
    private bool isContainer = false;

    private void Start()
    {
        itemUITemplate.gameObject.SetActive(false);
        closeButton.onClick.AddListener(CloseInventory);
        gameObject.SetActive(false);
        // LayoutRebuilder.ForceRebuildLayoutImmediate(parametersContainer.GetComponent<RectTransform>());
    }

    private void CloseInventory()
    {
        Debug.Log("clicked");
        InventoryUIController.Instance.CloseContainer();
        InventoryUIController.Instance.ClosePlayer();
    }

    public void DisplayInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshUI();
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
            itemUI.SetItem(item, isContainer);
            itemUI.gameObject.SetActive(true);
        }
    }

    private void UpdateWeight()
    {
        totalWeightText.gameObject.SetActive(!isContainer);
        weightIcon.gameObject.SetActive(!isContainer);
        totalWeightText.text = $"{Mathf.Round(currentWeightAmount * 10.0f)*0.1f}/{maxWeightAmount}";

        // Unity jank, have to update the horizontal layout group
        // LayoutRebuilder.ForceRebuildLayoutImmediate(parametersContainer.GetComponent<RectTransform>());

    }
    private void UpdateSlots()
    {
        totalSlotText.text = $"{Mathf.Round(currentSlotAmount * 10.0f)*0.1f}/{maxSlotAmount}";

        // Unity jank, have to update the horizontal layout group
        // LayoutRebuilder.ForceRebuildLayoutImmediate(parametersContainer.GetComponent<RectTransform>());
    }

    public void RefreshUI()
    {
        maxSlotAmount = inventory.MaxSlots;
        maxWeightAmount = inventory.MaxWeight;
        currentSlotAmount = inventory.CurrentSlots;
        currentWeightAmount = inventory.CurrentWeight;
        isContainer = inventory.IsContainer;
        inventoryName.text = inventory.InventoryName;
        UpdateSlots();
        UpdateWeight();
        InitializeItems(inventory.Items);
        LayoutRebuilder.ForceRebuildLayoutImmediate(parametersContainer.GetComponent<RectTransform>());
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
