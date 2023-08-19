using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Gear")]
public class GearObjectData : Object.ObjectData
{
    public override void AssignItemToPlayer(Game.Player.Inventory.PlayerEquipmentManager equipmentManager, InventoryObjectButton clickedSlot){
        equipmentManager.AssignGearItem(this, equipmentSlot, clickedSlot);
    }
    public override void UnassignItemFromPlayer(Game.Player.Inventory.PlayerEquipmentManager equipmentManager){
        equipmentManager.UnassignGearItem(this, equipmentSlot);
    }

    public enum GearClass {
        light,
        medium,
        heavy
    }

    [Header("armour settings")]
    public GearClass gearClass;

    public int equipmentSlot;
        // 1 = helmet
        // 2 = chest
        // 3 = arms
        // 4 = legs
        // 5 = class item(pending)

    public float defense;
}
