using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScript : MonoBehaviour
{
    [SerializeField] private Object.ObjectData obj;
    public void GiveObject() {
        Player.InventorySystem.instance.Add(obj);
    }
    public void TakeObject() {
        Player.InventorySystem.instance.Remove(obj);
    }
}
