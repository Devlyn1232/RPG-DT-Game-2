using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Items/Weapon")]
public class WeaponObjectData : Object.ObjectData
{
    public override void AssignItemToPlayer(){

    }

    public enum weaponType
    {
        Great,
        Short,
        Knife,
        Pole,
        Blunt,
        Unique
    };
    
    [Header("weapon settings")]
    public weaponType _weaponType;

    public float damage, range, penetration;
}
