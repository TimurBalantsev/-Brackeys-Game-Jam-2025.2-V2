
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
     }
     public void DisplayInventory(Inventory container, Inventory playerInventory)
     {
         ContainerUI.DisplayInventory(container);
         PlayerUI.DisplayInventory(playerInventory);
     }

     public void DisplayPlayerInventory(Player player)
     {
         PlayerUI.DisplayInventory(player.Inventory);
     }
     

     public void CloseContainer()
     {
         ContainerUI.HideInventory();
     }

     public void ClosePlayer()
     {
         PlayerUI.HideInventory();
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
             droppedItem.transform.position = Player.Instance.transform.position;
             droppedItem.Initialize(item);
         }
         return isItemDeleted;
     }

     public bool TransferItem(bool isInContainer, Item item)
     {
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
