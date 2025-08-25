
using UnityEngine;

public class Container : MonoBehaviour, Interactable
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    private Color defaultColor;
    
    private void Start()
    {
        defaultColor = spriteRenderer.color;
    }
    
    public void Interact(Player player)
    {
        InventoryUIController.Instance.DisplayInventory(inventory, player.Inventory);
    }

    public void Select(Player player, bool isSelected)
    {
        spriteRenderer.color = isSelected ? highlightColor : defaultColor;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
