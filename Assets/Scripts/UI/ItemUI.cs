using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Item item;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI slotText;
    [SerializeField] private Image typeIcon;

    [SerializeField] private Button TransferButton;
    [SerializeField] private Button DropButton;

    private bool isInContainer;

    private void Start()
    {
        TransferButton?.onClick.AddListener(TransferOnClick);
        DropButton?.onClick.AddListener(DropButtonOnClick);
    }

    private void DropButtonOnClick()
    {
        InventoryUIController.Instance.RemoveItemFromInventory(isInContainer, item);
    }

    private void TransferOnClick()
    {
        InventoryUIController.Instance.TransferItem(isInContainer, item);

    }

    // [SerializeField] private ItemSO defaultItem;
    // private void Start()
    // {
    //     if (item == null && defaultItem != null)
    //     {
    //         SetItem(defaultItem.CreateItem());
    //     }
    // }
    
    public void SetItem(Item item, bool isInContainer)
    {
        this.item = item;
        this.isInContainer = isInContainer;
        itemIcon.sprite = item.icon;
        nameText.text = item.name;
        descriptionText.text = item.description;
        weightText.text = $"{item.weight}";
        slotText.text = $"{item.slot}";

        if (typeIcon == null) return;

        typeIcon.color = ItemTypeHelper.Instance.GetItemTypeColor(item.itemType);
        typeIcon.sprite = ItemTypeHelper.Instance.GetItemTypeSprite(item.itemType);
    }
}
