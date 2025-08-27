
using UnityEngine;

public class Container : MonoBehaviour, Interactable
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private WeightedLootTableSO lootTable;
    [SerializeField] private int maxItemsSpawned;
    [SerializeField] private int minItemsSpawned;
    [SerializeField] private AudioClipSO openingSound;
    private Color defaultColor;
    
    private void Start()
    {
        defaultColor = spriteRenderer.color;
        if (lootTable == null) return;
        int amountItems = Random.Range(minItemsSpawned, maxItemsSpawned);
        for (int i = 0; i < amountItems; i++)
        {
            inventory.AddItem(lootTable.GetRandomItem());
        }
    }
    
    public void Interact(Player player)
    {
        InventoryUIController.Instance.DisplayInventory(inventory, player.Inventory);
        SoundManager.Instance.SpawnTempSoundSourceAtWorldSpacePoint(transform.position, openingSound.GetRandomAudioClipReference());
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
