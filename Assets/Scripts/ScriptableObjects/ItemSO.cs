using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public ItemType itemType;
    public float weight;
    public float slot;
    public Sprite icon;
    
    public Item CreateItem()
    {
        return new Item(this);
    }
}
