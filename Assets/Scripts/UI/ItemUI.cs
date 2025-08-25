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
    
    //TODO: add remove and add buttons depending on isContainer
    
    // [SerializeField] private ItemSO defaultItem;
    // private void Start()
    // {
    //     if (item == null && defaultItem != null)
    //     {
    //         SetItem(defaultItem.CreateItem());
    //     }
    // }
    
    public void SetItem(Item item)
    {
        this.item = item;
        itemIcon.sprite = item.icon;
        nameText.text = item.name;
        descriptionText.text = item.description;
        weightText.text = $"{item.weight}";
        slotText.text = $"{item.slot}";
        typeIcon.color = ItemTypeHelper.Instance.GetItemTypeColor(item.itemType);
        typeIcon.sprite = ItemTypeHelper.Instance.GetItemTypeSprite(item.itemType);
    }
}
