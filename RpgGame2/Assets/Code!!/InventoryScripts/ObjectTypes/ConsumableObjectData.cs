using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Consumable")]
public class ConsumableObjectData : Object.ObjectData
{
    public override void AssignItemToPlayer(Game.Player.Inventory.PlayerEquipmentManager equipmentManager, InventoryObjectButton clickedSlot){
        throw new System.NotImplementedException();
    }
    public override void UnassignItemFromPlayer(Game.Player.Inventory.PlayerEquipmentManager equipmentManager){
        throw new System.NotImplementedException();
    }

    [Header("consumable settings")]
    public float placeholder;
}
