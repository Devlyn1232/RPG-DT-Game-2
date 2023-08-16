using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Consumable")]
public class ConsumableObjectData : Object.ObjectData
{
    public override void AssignItemToPlayer(){

    }

    [Header("consumable settings")]
    public float placeholder;
}
