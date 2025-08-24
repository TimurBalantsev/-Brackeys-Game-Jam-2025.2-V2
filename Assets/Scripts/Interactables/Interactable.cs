using UnityEngine;

public interface Interactable
{
    public void Interact(Player player);
    public void Select(Player player, bool isSelected);
    public Transform GetTransform();
}
