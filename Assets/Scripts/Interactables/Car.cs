using System;
using UnityEngine;

public class Car : MonoBehaviour, Interactable
{
    public static Car Instance { get; private set; }

    public event Action OnInteract;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private AudioClipSO openingSound;
    private Color defaultColor;

    public Inventory Inventory => LoadingManager.Instance.persistantTruckInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple cars in scene");
        }
    }

    private void Start()
    {
        defaultColor = spriteRenderer.color;
    }

    public void Interact(Player player)
    {
        AudioManager.Instance.PlayTempSoundAt(transform.position, openingSound.GetRandomAudioClipReference());
        OnInteract?.Invoke();
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
