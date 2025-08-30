using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] Player player;
    [SerializeField] CircleCollider2D interactionCollider;
    
    private Interactable currentInteractable;
    private List<Interactable> interactablesInRange = new List<Interactable>(); //TODO: Maybe change to colliders

    
    private void Start()
    {
        interactionCollider.radius = interactRange;
        interactionCollider.includeLayers += interactableLayer;
        InputManager.Instance.OnInteract += HandleInteract;
    }
    
    private void Update()
    {
        SelectInteractable(GetClosestInteractable());
    }

    private void AddInteractableInRange(Interactable interactable)
    {
        if (!interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    public Interactable GetClosestInteractable()
    {
        if (interactablesInRange.Count == 0) return null;
        Interactable closest = null;
        float closestDistance = float.MaxValue;
        foreach (Interactable interactable in interactablesInRange)
        {
            Vector2 playerPosition = player.transform.position;
            Collider2D collider = interactable.GetTransform().GetComponent<Collider2D>();
            float distance = Vector2.Distance(playerPosition, collider.ClosestPoint((playerPosition)));
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        return closest;
    }
    
    public void RemoveInteractableInRange(Interactable interactable)
    {
        if (interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Remove(interactable);
        }
    }

    public void SelectInteractable(Interactable interactable)
    {
        if (currentInteractable == interactable) return;
        DeselectCurrentInteractable();
        currentInteractable = interactable;
        if (currentInteractable != null)
        {
            currentInteractable.Select(player, true); 
        }
    }
    
    public void DeselectCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Select(player, false);
            currentInteractable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable;
        if (other.TryGetComponent(out interactable))
        {
            AddInteractableInRange(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable;
        if (other.TryGetComponent(out interactable))
        {
            RemoveInteractableInRange(interactable);
        }
    }

    private void HandleInteract(object sender, EventArgs e)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(player);
        }
    }
}
