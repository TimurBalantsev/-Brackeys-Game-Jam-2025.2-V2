using UnityEngine;

public class Car : MonoBehaviour, Interactable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    private Color defaultColor;
    
    private void Start()
    {
        defaultColor = spriteRenderer.color;
    }
    

    public void Interact(Player player)
    {
        Debug.Log("car");
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
