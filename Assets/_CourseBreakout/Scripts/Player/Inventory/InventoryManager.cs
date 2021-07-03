using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public static int MAX_ITEMS = 5;

   public GameObject inventoryUI;

 
    List<InventorySlot> slots = new List<InventorySlot>(MAX_ITEMS);
    
    int selectedSlotIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        foreach (InventorySlot slot in inventoryUI.GetComponentsInChildren<InventorySlot>())
        {
            slots.Add(slot);
        }
    }

    public void SelectItem(int slotIndex)
    {
        if (slotIndex >= MAX_ITEMS || selectedSlotIndex == slotIndex)
            return;

        InventorySlot slot = slots[slotIndex];

        if (slot.GetItem() == null)
            return;
        
        foreach (InventorySlot s in inventoryUI.GetComponentsInChildren<InventorySlot>()) {
            s.GetComponent<Image>().CrossFadeColor(new Color(255, 255, 255, 1), 0.3f, true, true);
        }

        slot.GetComponent<Image>().CrossFadeColor(new Color(73, 0, 152, 30), 0.3f, true, true);
        selectedSlotIndex = slotIndex;
    }

    public void AddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.GetItem() == null) {
                slot.SetItem(item);
                break;
            }
        }
    }

    public void AddItem(Item item, int slotIndex) {
        if (slotIndex >= MAX_ITEMS)
            return;

        InventorySlot slot = slots[slotIndex];
        slot.SetItem(item);
    }

   public InventorySlot GetSelectedSlot()
    {
        if (selectedSlotIndex < 0)
            return null;

        return slots[selectedSlotIndex];
    }
}
