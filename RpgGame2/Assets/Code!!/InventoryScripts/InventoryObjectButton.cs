using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryObjectButton : MonoBehaviour
{
    public Object.ObjectData itemInSlot;

    public bool used = false;
    public void OnUISlotClick() {
        if (used == false) {
            Game.Player.InventorySystem.instance.SlotClicked(this);
            used = true;
        }
        else {
            Game.Player.InventorySystem.instance.SlotClicked(this);
            used = false;
        }
        
    }

    public void SetItemInSlot(Object.ObjectData _itemInSlot) {
        itemInSlot = _itemInSlot;
    }
}
