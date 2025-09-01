
using UnityEngine;

public class Container : MonoBehaviour, Interactable
{
    [SerializeField] private Inventory inventory;
    // [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private WeightedLootTableSO lootTable;
    [SerializeField] private int maxItemsSpawned;
    [SerializeField] private int minItemsSpawned;
    [SerializeField] private AudioClipSO openingSound;

    [SerializeField] private bool usePersistantCarInventory = false;

    private void Awake()
    {
        if (usePersistantCarInventory)
        {
            inventory = LoadingManager.Instance.persistantTruckInventory;
        }
    }

    private void Start()
    {
        if (spriteRenderers.Length == 0)
        {
            Debug.LogError($"{this} has no sprite rendrer");
        }
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
        AudioManager.Instance.PlayTempSoundAt(transform.position, openingSound.GetRandomAudioClipReference());
    }

    public void Select(Player player, bool isSelected)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = isSelected ? highlightColor : defaultColor;

        }
        // spriteRenderer.color = isSelected ? highlightColor : defaultColor;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
