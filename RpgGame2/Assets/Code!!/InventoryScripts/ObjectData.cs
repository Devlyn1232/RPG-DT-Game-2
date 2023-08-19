using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    public abstract class ObjectData : ScriptableObject {
        public enum ObjType
        {
            Resource,
            Weapon,
            Consumables,
            Gear
        };
        public string id;
        public string displayName;
        public Sprite icon;
        public ObjType type;
        public bool stackable;
        public GameObject prefab;

        public abstract void AssignItemToPlayer(Game.Player.Inventory.PlayerEquipmentManager equipmentManager, InventoryObjectButton clickedSlot);

        public abstract void UnassignItemFromPlayer(Game.Player.Inventory.PlayerEquipmentManager equipmentManager);
    }
}
