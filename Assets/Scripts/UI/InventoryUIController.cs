
 using System;
 using UnityEngine;

 public class InventoryUIController : MonoBehaviour
 { 
     public static InventoryUIController Instance;
     [SerializeField] private InventoryUI ContainerUI;
     [SerializeField] private InventoryUI PlayerUI;
     
     [SerializeField] private DroppedItem droppedItemPrefab;

     private void Awake()
     {
         if (Instance != null)
         {
             Debug.LogError("Inventory UI controller dupilcates in scene");
         }
         Instance = this;
     }

     public void SetPlayerCanMove()
     {
         Player.Instance.canMove = !PlayerUI.gameObject.activeSelf && (ContainerUI == null || !ContainerUI.gameObject.activeSelf);
     }

     public void TogglePlayerInventory(Player player)
     {
         if (PlayerUI.gameObject.activeSelf)
         {
             ClosePlayer();
         }
         else
         {
             DisplayPlayerInventory(player);
         }

         SetPlayerCanMove();
     }
     public void DisplayInventory(Inventory container, Inventory playerInventory)
     {
         ContainerUI.DisplayInventory(container);
         PlayerUI.DisplayInventory(playerInventory);
         SetPlayerCanMove();
     }

     public void DisplayPlayerInventory(Player player)
     {
         PlayerUI.DisplayInventory(player.Inventory);
         SetPlayerCanMove();
     }
     

     public void CloseContainer()
     {
         ContainerUI.HideInventory();
         SetPlayerCanMove();
     }

     public void ClosePlayer()
     {
         PlayerUI.HideInventory();
         SetPlayerCanMove();
     }

     public void RefereshUI()
     {
         PlayerUI.RefreshUI();
         ContainerUI.RefreshUI();
     }

     public bool RemoveItemFromInventory(bool isContainer, Item item)
     {
         InventoryUI targetInventory = isContainer ? ContainerUI : PlayerUI;
         bool isItemDeleted = targetInventory.GetInventory().RemoveItem(item);
         if (isItemDeleted)
         {
             targetInventory.RefreshUI();
             DroppedItem droppedItem = Instantiate(droppedItemPrefab);
             droppedItem.transform.position = Player.Instance.transform.position; //TODO: make it so player isn't a singleton
             droppedItem.Initialize(item);
         }
         return isItemDeleted;
     }

     public bool TransferItem(bool isInContainer, Item item)
     {
         if (!ContainerUI.gameObject.activeSelf || !PlayerUI.gameObject.activeSelf) return false;
         Inventory origin = isInContainer ? ContainerUI.GetInventory() : PlayerUI.GetInventory();
         Inventory target = isInContainer ? PlayerUI.GetInventory() : ContainerUI.GetInventory();
         if (!origin.RemoveItem(item)) return false;
         if (target.AddItem(item))
         {
             RefereshUI();
             return true;
         }
         origin.AddItem(item);

         return false;
     }
 }
