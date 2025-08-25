using System;
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
    private bool isContainer = false;
    //inv list
    // private void Show(Inventory inventory)
    // {
    //     
    // }

    private void Start()
    {
        UpdateSlots();
        UpdateWeight();
    }

    public void DisplayInventory(Inventory inventory)
    {
        
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
