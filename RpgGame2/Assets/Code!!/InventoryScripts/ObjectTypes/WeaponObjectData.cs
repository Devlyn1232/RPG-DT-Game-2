using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Weapon")]
public class WeaponObjectData : Object.ObjectData
{
    public override void AssignItemToPlayer(){

    }
    public float damage, range;
}
