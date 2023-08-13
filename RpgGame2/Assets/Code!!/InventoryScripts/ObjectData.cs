using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    public abstract class ObjectData : ScriptableObject {
        public enum objType
        {
            Resource,
            Weapon,
            Consumables,
            Gear
        };
        public string id;
        public string displayName;
        public Sprite icon;
        public objType type;
        public bool stackable;
        public GameObject prefab;

        public abstract void AssignItemToPlayer();
    }
}
