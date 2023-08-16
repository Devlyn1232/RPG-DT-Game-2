using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Gear")]
public class GearObjectData : Object.ObjectData
{
    public override void AssignItemToPlayer(){

    }

    public enum GearClass {
        light,
        medium,
        heavy
    }

    [Header("armour settings")]
    public GearClass gearClass;

    public int equipmentSlot;
        // 0 = helmet
        // 1 = chest
        // 2 = arms
        // 3 = legs
        // 4 = class item(pending)

    public float defense;
}
