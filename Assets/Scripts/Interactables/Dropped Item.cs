using System;
using UnityEngine;

public class DroppedItem : MonoBehaviour, Interactable
{
    private Item item;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    private Color defaultColor;

    [SerializeField] private ItemSO ForceItemSO;

    private void Start()
    {
        if (ForceItemSO)
        {
            Initialize(ForceItemSO.CreateItem());
        }

        defaultColor = spriteRenderer.color;
    }

    public void Initialize(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.icon;
    }
    public void Interact(Player player)
    {
        if (player.Inventory.AddItem(item))
        {
            Destroy(gameObject);
        }
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
